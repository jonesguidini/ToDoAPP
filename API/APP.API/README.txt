PROJETO BASE

Este repositório contém uma estrutura base para criação de outras aplicações em .NET CORE web API com DDD, Services, Repositories e outros Design Patters.

Ao criar um novo repositório a partir deste é necessário seguir o seguinte fluxo:

Atualizar banco de dados:
- Criar Entidades em 'APP.Domain.Entities' herdando de 'BaseEntity' ou 'DeletedEntity' caso a entidade possa ter exclusão lógica
- Criar DTO's das Entidades em 'APP.Domain.DTOs'
- Criar VM's das Entidades em 'APP.Domain.VMs'
- Criar FluentValidation das Entities em 'APP.Domain.Entities.FluentValidation'
- Criar Mappings das Entidades para Fluent API com o banco de dados em 'APP.Data.Mappings'
- Configurar connection string do banco de dados na 'appsettings.json'
- Adicionar migrations com o comando 'Add-Migration nomeDaMigration'
- Atualizar banco de dados com as migrations criada com o comando 'Update-Database'

REPOSITORIOES
- Criar interface para os novos repositorios em 'APP.Domain.Contracts.Repositories'
- Criar classes dos repositorios implementando as interfaces criada acima em 'APP.Data.Repositories'

SERVIÇOS
- Criar interface para os novos serviços em 'APP.Domain.Contracts.Services'
- Criar classes das services implementando as interfaces e repositorios criados anteriormente em 'APP.Business.Services'

CONTROLLERS
- Criar controllers para as novas entidades importanto as services criadas anteriormente
