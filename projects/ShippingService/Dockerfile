FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy csproject and restore as distinct layers
COPY ./projects/ShippingService/*.csproj ../../projects/ShippingService/
COPY ./packages/MessageClient/*.csproj ../../packages/MessageClient/
COPY ./packages/Messages/*.csproj ../../packages/Messages/
COPY ./packages/Repository/*.csproj ../../packages/Repository/

WORKDIR /App
RUN dotnet restore ../../projects/ShippingService/ShippingService.csproj

# Copy everything else and build
COPY ./projects/ShippingService ../../projects/ShippingService
COPY ./packages/MessageClient ../../packages/MessageClient
COPY ./packages/Messages ../../packages/Messages
COPY ./packages/Repository ../../packages/Repository

WORKDIR /App
RUN dotnet build ../../projects/ShippingService/ShippingService.csproj -c Release -o /App

FROM mcr.microsoft.com/dotnet/runtime:7.0
WORKDIR /App
COPY --from=build-env /App .
ENTRYPOINT ["dotnet", "ShippingService.dll"]