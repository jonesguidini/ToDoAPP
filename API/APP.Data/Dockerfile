FROM mysql
ADD ./Seed/script_fulldump_inicial_db.sql /docker-entrypoint-initdb.d/script.sql
RUN chmod -R 775 /docker-entrypoint-initdb.d

ENV MYSQL_ROOT_PASSWORD mudar123