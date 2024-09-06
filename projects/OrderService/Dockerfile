FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy csproject and restore as distinct layers
COPY ./projects/OrderService/*.csproj ../../projects/OrderService/
COPY ./packages/MessageClient/*.csproj ../../packages/MessageClient/
COPY ./packages/Messages/*.csproj ../../packages/Messages/
COPY ./packages/Repository/*.csproj ../../packages/Repository/
COPY ./packages/Mapper/*.csproj ../../packages/Mapper/

WORKDIR /App
RUN dotnet restore ../../projects/OrderService/OrderService.csproj

# Copy everything else and build
COPY ./projects/OrderService ../../projects/OrderService
COPY ./packages/MessageClient ../../packages/MessageClient
COPY ./packages/Messages ../../packages/Messages
COPY ./packages/Repository ../../packages/Repository
COPY ./packages/Mapper ../../packages/Mapper

WORKDIR /App
RUN dotnet build ../../projects/OrderService/OrderService.csproj -c Release -o /App

FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /App
COPY --from=build-env /App .
ENTRYPOINT ["dotnet", "OrderService.dll"]