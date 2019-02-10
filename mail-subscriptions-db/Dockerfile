FROM microsoft/mssql-server-linux
ENV SA_PASSWORD=abbTest123
ENV ACCEPT_EULA=Y
COPY entrypoint.sh entrypoint.sh
COPY SqlCmdStartup.sh SqlCmdStartup.sh
COPY script.sql script.sql
RUN chmod +x ./SqlCmdStartup.sh
CMD /bin/bash ./entrypoint.sh