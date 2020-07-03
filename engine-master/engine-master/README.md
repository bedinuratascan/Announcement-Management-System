# Recommendation Engine
Run;
```
python3 -m venv venv
source venv/bin/activate
python -m pip install -r requirements.txt
python app.py
celery worker -A app.celery --loglevel=info
```