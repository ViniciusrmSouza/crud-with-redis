FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5169

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ConnectionStrings__DefaultConnection="Server=mysql-db;DataBase=db_containerizado;Uid=root;Pwd=admin"

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CRUDDockerizado.csproj", "./"]
RUN dotnet restore "CRUDDockerizado.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "CRUDDockerizado.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRUDDockerizado.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUDDockerizado.dll"]
