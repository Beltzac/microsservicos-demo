## Pré-requisitos

* Computador
* Internet
* .NET Core 3.1
* Docker

## Como executar

* Clonar o repositório

* Rodar com apenas uma instância do microsserviço (ele lê as próprias mensagens):

  >docker-compose up

* Rodar com várias instâncias do microsserviço:

  > docker-compose up --scale hello_world_app=5

* Para reconstruir a imagem caso existam alterações no projeto:

  > docker-compose up --build
