## Desafio para Back-end Developer na DoroTech - C# .NET

## Configurações appsettings.json / appsettings.Development.json:
* ConnectionStrings:DefaultConnection: Conexão com o banco de dados (SQL Server)
* JwtSettings: configurações relacionadas a autenticação com JWT
* InitialiseDatabase: Caso esteja habilitado, na inicialização do projeto será executada as migrations e realizar uma carga inicial no banco de dados.
* Logging:Elasticsearch:Host: Host do elasticseach para envio dos logs

## Usuário Admin
Caso esteja utilizando a opção InitialiseDatabase, já será criado um usuário padrão
* user: administrator@localhost
* password: Administrator1! 

## Depêndencias:
### SQL Server
`docker run --name inventory -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Inventory!123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest`

### Elasticsearch (opcional)
`docker run -p 5601:5601 -p 9200:9200 -e "discovery.type=single-node" docker.elastic.co/elasticsearch/elasticsearch:7.15.2`
Caso esteja utilizando o elasticsearch, os logs poderão ser visualizados em `http://localhost:9200/_search`. 
Como alternativa, poderá ser utilizado o Kibana para conectar ao elasticsearch e obter a visualização dos logs de forma mais amigável.