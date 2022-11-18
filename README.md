# Library API

## Como executar
Para executar essa API, é necessário estar rodando uma instância do SQL Server. Tenso isso em mente, é necessário trocar a connection string do arquivo LibraryAPI/API/Program.cs pela connection string do seu banco local.

Em seguida, é necessário atualizar  o banco de dados com as migrations existentes, entrando na pasta LibraryAPI/API e executando o comando:

```
Update-Database
```

Ou, utilizando a dotnet cli

```
dotnet ef database update
```

Para finalizar, podemos executar a aplicação pelo visual studio ou executar o comando abaixo para rodar a aplicação localmente:

```
dotnet run
```

Para acessá-la, digite no navegador a url da porta seguido de /swagger, por exemplo:

```
https://localhost:7044/swagger
```

## Utilizando a API
Para utilizar a API, é necessário, antes de tudo, criar um usuário. Existem dois tipos de usuário, o básico e o administrador. Um usuário do tipo básico pode apenas consultar os livros existentes, já um administrador pode inserir, atualizar e apagar livros, além de poder ver os livros existentes.

Para criar um novo usuário, chame o endpoint a seguir (Lembrando de passar Role = 1 para Basico e Role = 2 para Administrador).

```
https://localhost:7044/api/Users - POST
```

Feito isso, é necessário chamar o endpoint de login, para obter um Token e utilizar a API:

```
https://localhost:7044/api/Users/login - POST
```

Por último, pegue o token que foi lhe devolvido na requisição e insira ele no Authorize do Swagger, liberando as outras requisições da API.