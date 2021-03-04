#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Nvg.EmailBackgroundTask/Nvg.EmailBackgroundTask.csproj", "Nvg.EmailBackgroundTask/"]
COPY ["BuildingBlocks/EventBus/EventBus/EventBus.csproj", "BuildingBlocks/EventBus/EventBus/"]
COPY ["Nvg.EmailService/Nvg.EmailService.csproj", "Nvg.EmailService/"]
COPY ["BuildingBlocks/EventBus/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "BuildingBlocks/EventBus/EventBusRabbitMQ/"]
RUN dotnet restore "Nvg.EmailBackgroundTask/Nvg.EmailBackgroundTask.csproj"
COPY . .
WORKDIR "/src/Nvg.EmailBackgroundTask"
RUN dotnet build "Nvg.EmailBackgroundTask.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Nvg.EmailBackgroundTask.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nvg.EmailBackgroundTask.dll"]