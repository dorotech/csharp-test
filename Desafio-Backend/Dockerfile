#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Desafio-Backend/API.csproj", "Desafio-Backend/"]
COPY ["Application/Backend.Application.csproj", "Application/"]
COPY ["Persistence/Backend.Persistence.csproj", "Persistence/"]
COPY ["Domain/Backend.Domain.csproj", "Domain/"]
RUN dotnet restore "Desafio-Backend/API.csproj"
COPY . .
WORKDIR "/src/Desafio-Backend"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]