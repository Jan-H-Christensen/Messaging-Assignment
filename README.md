# ECommerce Assignment

## Overview

This is a example of messaging in .NET 7 using EasyNetQ.
There are left several TODOs in the code for you to complete. The projects themselves have some design flaws, see if you can spot them.

## Technologies

- .NET 7, .NET 7 SDK
- EasyNetQ

## Building and Running the Application

The application can be built and run using Docker or .NET 7 SDK.

### Prerequisites

1. Install [Docker](https://docs.docker.com/engine/install/)
2. Install [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
3. Clone this repository to your local machine

### Local Build

To build each of the services, you need to navigate to the project folder and run the `dotnet build` command.
Following services are available:
- OrderService
- ShippingService
- StockService
- StoreAPI

### Docker Build

Before building the microservice images and running them using Docker, you need to have a running RabbitMQ instance.

The microservices and RabbitMQ will be running in the same network, so you need to create a network first:

```bash
docker network create microservices_net
```

```bash
docker run -d --network microservices_net --hostname rabbitmq --name rabbitmq -p 15672:15672 -p 5672:5672 rabbitmq:3.12-management
```
It will build and run a RabbitMQ container with the same hostname and network that microservice containers will use for connecting.

Once you have RabbitMQ running, you can build and run the microservices using the following command in the project root folder:

```bash
docker-compose up -d
```