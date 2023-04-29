#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./WebApi/WebApi.csproj", "WebApi/"]
COPY ["./WebApi.Entities/WebApi.Entities.csproj", "WebApi.Entities/"]
COPY ["./WebApi.Repository.Interfaces/WebApi.Repository.Interfaces.csproj", "WebApi.Repository.Interfaces/"]
COPY ["./WebApi.Repository.PostgreSQL/WebApi.Repository.PostgreSQL.csproj", "WebApi.Repository.PostgreSQL/"]
COPY ["./WebApi.Services/WebApi.Services.csproj", "WebApi.Services/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
