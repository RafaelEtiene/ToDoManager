# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o arquivo da solução (.sln) para o contêiner
COPY ToDoManager.WebApi.sln ./

# Copia o .csproj para o contêiner
COPY ToDoManager.WebApi/ToDoManager.WebApi.csproj ./ToDoManager.WebApi/ToDoManager.WebApi.csproj

# Restaura as dependências do projeto .csproj
RUN dotnet restore ./ToDoManager.WebApi/ToDoManager.WebApi.csproj

# Copia o restante dos arquivos para o contêiner
COPY . ./

# Publica a aplicação para a pasta /out
RUN dotnet publish ./ToDoManager.WebApi/ToDoManager.WebApi.csproj -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia a build da etapa anterior para a etapa de runtime
COPY --from=build /out ./

# Expõe a porta para a aplicação
EXPOSE 8080

# Comando para executar a aplicação
ENTRYPOINT ["dotnet", "ToDoManager.WebApi.dll"]
