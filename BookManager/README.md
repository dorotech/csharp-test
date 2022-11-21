# BookManeger Test

This application is generated using C# And Visual Studio Code

Projeto para controle de livros

## Dependencies

- Microsoft.EntityFrameworkCore

```sh
dotnet restore
Database Postgre
Set Conect string in appsettings.json
dotnet ef database update --context DataContext
```

## Run the application

```sh
dotnet run
```

## Tests

```sh
Coleção do postman anexa no projeto

```

## Ecosistema da Api

```

Ide de desenvolvimento Visual Code
Linguagem C#
.Net FrameWork 6
Docker-distribuição
Banco postegrer  (mas poderia seu oracle ou sqlserver)
Construção do banco EF-Migrations
Distribuição - Docker
Autenticação e autorização: JWT
Documentação da Api - Swegger
Teste Api - Postman (Collection anexa)
Revisão de codigo foi utilisado a extensão  eslint
Padroes de projeto:(explicitar )
    - Factory  src/service/factory.report.ts
    - Singleton src/services/singleton.ts
    - injeção de dependencia -> src/services/
    - Princípio de responsabilidade única -> src/services/


```

## Trello atividades

```sh

#1 - Analise e entendimento da demanda
#2 - Dividir para conquistar - dividir o projeto em atividades via trello
#3 - Validar escopo
#4 - Criar setup do projeto
#5 - Gerar o Diagrama classes
#6 - criar a base de dados
#7 - Criar os migrates
#8 - Criar os seeds
#9 - configurar o docker
#10- Gerar os modelos
#11- gerar as controller-limpar
#13- Configurar jwt
#15- Gerar Chave
#16- Testar e refatorar
#17- Atualizar o Re
#18- Validações
#19-pipline

```

## Check List Macro

```sh

- Modelagem DR -OK
- Swagger - OK
- Jwt -OK
- gitignore -OK
- injeção de dependencia -ok
- Princípio de responsabilidade única -ok
- Entity -ok
- Crud-OK
- Banco Pstegre -ok
- Migrates -ok
- Seeds - ok
- Log - ok
- Paginação-ok
- SOLID -ok
- Factory-ok
- Singleton-ok
- TDD-ok
- VIDEO

dotnet ef migrations add Migration5 --context DataContext
dotnet ef database update --context DataContext
```

## Video Projeto execultaldo

```sh
 https://www.youtube.com/

```
