# ToDoManager

docker run -d --name sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" -p 1433:1433 -v sqlserver_data:/var/opt/mssql --restart always mcr.microsoft.com/mssql/server:2022-latest