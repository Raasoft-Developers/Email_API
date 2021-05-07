#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Nvg.Api.Email/Nvg.Api.Email.csproj", "Nvg.Api.Email/"]
COPY ["BuildingBlocks/EventBus/EventBus/EventBus.csproj", "BuildingBlocks/EventBus/EventBus/"]
COPY ["Nvg.EmailService/Nvg.EmailService.csproj", "Nvg.EmailService/"]
COPY ["BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "BuildingBlocks/EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "Nvg.Api.Email/Nvg.Api.Email.csproj"
COPY . .
WORKDIR "/src/Nvg.Api.Email"
RUN dotnet build "Nvg.Api.Email.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nvg.Api.Email.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nvg.Api.Email.dll"]