FROM scipy/scipy-dev:latest
RUN apt-get install -y freetds-dev freetds-bin unixodbc-dev tdsodbc
RUN mkdir -p /usr/lib/odbc
RUN echo "[FreeTDS]" > /etc/odbcinst.ini
RUN echo "Description=FreeTDS Driver" >> /etc/odbcinst.ini
RUN echo "Driver=/usr/lib/x86_64-linux-gnu/odbc/libtdsodbc.so" >> /etc/odbcinst.ini
WORKDIR /project
ADD . /project
RUN pip install -r requirements.txt