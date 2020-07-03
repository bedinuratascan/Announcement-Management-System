from faker import Faker
import pyodbc
import random
from datetime import datetime


def run_sql(sql):
    connection = pyodbc.connect(
        server='localhost',
        database='AnnouncApp',
        user='sa',
        password='Asdqwe123.,',
        tds_version='7.4',
        port=1433,
        driver='FreeTDS'
    )
    cursor = connection.cursor()
    try:
        cursor.execute(sql)
        connection.commit()
    except Exception as e:
        print(sql)
    cursor.close()
    connection.close()


if __name__ == '__main__':
    fake = Faker()

    print("Generating users...")
    for _ in range(50):
        email = fake.email()
        password = fake.name()
        first_name = fake.first_name()
        last_name = fake.last_name()
        command = """
        INSERT INTO [User] (Email, Password, Name, SurName)
        VALUES ('{}', '{}', '{}', '{}')
        """.format(email, password, first_name, last_name).strip()
        run_sql(command)

    print("Generating announcements...")
    for index in range(500):
        title = "{} - {}".format(index, fake.company())
        contents = fake.text()
        user = random.randint(1, 50)
        command = """
        INSERT INTO [Announcement] (UserId, Title, Contents, CreatedAt, UpdatedAt) 
        VALUES ({}, '{}', '{}', '{}', '{}')
        """.format(user, title, contents, datetime.now(), datetime.now()).strip()
        run_sql(command)

    print("Generating likes...")
    for user in range(random.randint(1, 50)):
        for _ in range(random.randint(1, 20)):
            announcement = random.randint(1, 500)
            command = '''
                    INSERT INTO [Like] (UserId, AnnouncementId) 
                    VALUES ({}, {}) 
                    '''.format(user + 1, announcement).strip()
            run_sql(command)

    print("Generating interactions...")
    for user in range(50):
        for _ in range(random.randint(1, 150)):
            announcement = random.randint(1, 500)
            action = ("VIEW", "LIKE")[random.randint(0, 1)]
            command = """
            INSERT INTO [Interaction] (UserId, AnnouncementId, Type) 
            VALUES({}, {}, '{}') 
            """.format(user + 1, announcement, action).strip()
            run_sql(command)
