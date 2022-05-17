# Dotnet 6 microservices project

## What's the project about
- It has been built to work as a part of a backend of a simple game
- The functions are about having a crud for player items and giving items to player

## Technologies
- Docker (mongo db, rabbitmq)
- docker-compose
- RabbitMQ
  - Concept of _publisher_ and _consumer_ for message broker
  - Advanced Message Queuing Protocol(AMQP)
- .NET 6
- dotnet records
- Circuit breaker pattern on microservices

### Play.Catalog
- Service that provides game item crud
- It is possible to create, update and delete items
- Contains a Contract for the message broker

### Play.Inventory
- Service that provides inventory service for a player
- It is possible to associate an item to an player inventory

### Play.Common
- Nuget package library with many configurations like
  - IRepository
  - IEntity
  - Configurations for MongoDB
  - Configurations for MassTransit framework that was used to implement RabbitMQ message broker service

### Play.Infra
- Contains the infra for the project
- There is a _docker-compose.yml_ file to build up the infra for the code
  - The infra consists in a mongodb database image and rabbitmq image instance in docker

### packages
- Contains the nuget packages for the project


## How to build the project
- *Requires Docker*
- Inside Play.Infra dir, run `docker-compose up` or `docker-compose up -d` for less information on terminal
- Inside Play.Catalog/src/Play.Catalog.Services, run `dotnet restore` then `dotnet run`
- Inside Play.Inventory/src/Play.Inventory.Services, run `dotnet restore`, then `dotnet run`