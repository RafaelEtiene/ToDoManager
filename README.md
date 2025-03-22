
# To Do Manager

## Introdução
Este projeto tem como objetivo fornecer uma solução para gerenciar listas de tarefas. Ele é composto por uma API em ASP.NET Core para o back-end, uma aplicação front-end desenvolvida em Angular e um banco de dados, que pode ser executado utilizando Docker. A API permite criar, editar, listar e excluir tarefas, enquanto a interface do usuário oferece uma maneira simples e intuitiva para interagir com essas tarefas.

## Como Rodar a Aplicação

### 1. Como Rodar a API em ASP.NET Core

#### Pré-requisitos
- .NET SDK 8.0 ou superior
- Banco de dados configurado no Docker
- Docker desktop

#### Passos para rodar a API
1. **Clone o repositório do projeto:**
   ```bash
   git clone https://github.com/RafaelEtiene/ToDoManager.git
   cd \ToDoManager\ToDoManager.WebApi
   ```

2. **Restaure as dependências do projeto:**
   Se você ainda não tiver restaurado as dependências do projeto, execute o comando abaixo:
   ```bash
   dotnet restore
   ```

3. **Configure o banco de dados:**
   Rodar comando abaixo
      ```bash
      docker run -d --name sqlserver -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Password123" -p 1433:1433 -v sqlserver_data:/var/opt/mssql --restart always mcr.microsoft.com/mssql/server:2022-latest
      ```


4. Migrations
Migração esta configurada para rodar automaticamente.

5. **Inicie o servidor da API:**
   Para rodar a API, execute o seguinte comando:
   ```bash
   cd \ToDoManager\ToDoManager.WebApi\ToDoManager.WebApi
   dotnet run --environment "Development"
   ```


### 2. Como Rodar o Front-End com Angular

#### Pré-requisitos
- Node.js e npm (ou yarn) instalados
- Angular CLI instalado globalmente

#### Passos para rodar o front-end
1. **Acessar o repositório do front-end:**
   ```bash
   cd \ToDoManager\ToDoManager.Web
   ```

2. **Instale as dependências do Angular:**
   ```bash
   npm install
   ```

3. **Inicie o servidor de desenvolvimento Angular:**
   Para rodar o front-end, execute o comando abaixo:
   ```bash
   ng serve
   ```

   O front-end estará disponível na URL `http://localhost:4200`.


### 4. Como Rodar os Testes da API com xUnit

#### Pré-requisitos
- .NET SDK 8.0 ou superior

#### Passos para rodar os testes
1. **Acessar diretório dos testes **
   cd \ToDoManager\ToDoManager.WebApi\ToDoManager.Test

1. **Restaure as dependências dos testes:**
   ```bash
   dotnet restore
   ```

3. **Execute os testes:**
   Para rodar os testes da API, execute o seguinte comando:
   ```bash
   dotnet test
   ```

## Conclusão
Agora que você configurou tanto o back-end quanto o front-end e pode rodar os testes, está pronto para interagir com o sistema e gerenciar suas listas de tarefas. Esse projeto oferece uma boa base para a criação e gerenciamento de tarefas, com uma API flexível e uma interface moderna e responsiva desenvolvida com Angular.
