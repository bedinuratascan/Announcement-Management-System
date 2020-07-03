import pandas
import pyodbc
from flask import Flask, jsonify
from celery import Celery
from scipy import sparse
import numpy
import implicit
from sklearn.preprocessing import MinMaxScaler

app = Flask(__name__)
app.config.update(
    CELERY_BROKER_URL='redis://redis:6379',
    CELERY_RESULT_BACKEND='redis://redis:6379'
)

celery = Celery(app.name,
                broker=app.config['CELERY_BROKER_URL'],
                backend=app.config['CELERY_RESULT_BACKEND'])
celery.conf.update(app.config)


@celery.task
def run_async(user_id):
    user_id = int(user_id)
    connection = pyodbc.connect(
        server='mssql',
        database='AnnouncApp',
        user='sa',
        password='Asdqwe123.,',
        tds_version='7.4',
        port=1433,
        driver='FreeTDS'
    )
    sql_command = '''
    SELECT
    Interaction.AnnouncementId as contentId,
    Interaction.UserId as personId,
    Interaction.Type as eventType,
    Announcement.Contents as contents
    FROM Interaction
    INNER JOIN Announcement on Interaction.AnnouncementId = Announcement.Id;
            '''.strip()
    df = pandas.read_sql(sql_command, connection)
    event_type_strength = {
        'VIEW': 1.0,
        'LIKE': 2.0
    }
    df['eventStrength'] = df['eventType'].apply(lambda x: event_type_strength[x])
    df = df.drop_duplicates()
    grouped_df = df.groupby(['personId', 'contentId', 'contents']).sum().reset_index()
    grouped_df['contents'] = grouped_df['contents'].astype("category")
    grouped_df['personId'] = grouped_df['personId'].astype("category")
    grouped_df['contentId'] = grouped_df['contentId'].astype("category")
    grouped_df['person_id'] = grouped_df['personId'].cat.codes
    grouped_df['content_id'] = grouped_df['contentId'].cat.codes
    sparse_content_person = sparse.csr_matrix(
        (grouped_df['eventStrength'].astype(float), (grouped_df['content_id'], grouped_df['person_id']))
    )
    sparse_person_content = sparse.csr_matrix(
        (grouped_df['eventStrength'].astype(float), (grouped_df['person_id'], grouped_df['content_id']))
    )
    model = implicit.als.AlternatingLeastSquares(factors=20, regularization=0.1, iterations=50)
    alpha = 15
    data = (sparse_content_person * alpha).astype('double')
    model.fit(data)
    person_vecs = sparse.csr_matrix(model.user_factors)
    content_vecs = sparse.csr_matrix(model.item_factors)

    cursor = connection.cursor()
    cursor.execute('DELETE FROM [Recommendation] WHERE UserId={}'.format(user_id))
    connection.commit()
    cursor.close()

    person_index = grouped_df.person_id.loc[grouped_df.personId == user_id].iloc[0]
    person_interactions = sparse_person_content[person_index, :].toarray()
    person_interactions = person_interactions.reshape(-1) + 1
    person_interactions[person_interactions > 1] = 0
    rec_vector = person_vecs[person_index, :].dot(content_vecs.T).toarray()
    min_max = MinMaxScaler()
    rec_vector_scaled = min_max.fit_transform(rec_vector.reshape(-1, 1))[:, 0]
    recommend_vector = person_interactions * rec_vector_scaled
    content_idx = numpy.argsort(recommend_vector)[::-1][:8]
    announcements = []

    for idx in content_idx:
        announcements.append(grouped_df.contentId.loc[grouped_df.content_id == idx].iloc[0])

    recommendations = pandas.DataFrame({'announcements': announcements})

    for recommendation_id in recommendations.announcements:
        command = '''
        INSERT INTO [Recommendation] (UserId, AnnouncementId) 
        VALUES ({}, {})
        '''.format(int(user_id), int(recommendation_id)).strip()
        try:
            cursor = connection.cursor()
            cursor.execute(command)
            connection.commit()
            cursor.close()
        except Exception as e:
            print("{} - {}".format(str(e), command))

    connection.close()


@app.route('/<user_id>')
def run(user_id):
    run_async.delay(user_id)
    return jsonify({}), 200


if __name__ == '__main__':
    app.run(debug=True, host="0.0.0.0", port="7878")
