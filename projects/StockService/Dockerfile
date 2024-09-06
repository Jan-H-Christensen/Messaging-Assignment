FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy csproject and restore as distinct layers
COPY ./projects/StockService/*.csproj ../../projects/StockService/
COPY ./packages/MessageClient/*.csproj ../../packages/MessageClient/
COPY ./packages/Messages/*.csproj ../../packages/Messages/
COPY ./packages/Repository/*.csproj ../../packages/Repository/

WORKDIR /App
RUN dotnet restore ../../projects/StockService/StockService.csproj

# Copy everything else and build
COPY ./projects/StockService ../../projects/StockService
COPY ./packages/MessageClient ../../packages/MessageClient
COPY ./packages/Messages ../../packages/Messages
COPY ./packages/Repository ../../packages/Repository

WORKDIR /App
RUN dotnet build ../../projects/StockService/StockService.csproj -c Release -o /App

FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /App
COPY --from=build-env /App .
ENTRYPOINT ["dotnet", "StockService.dll"]