FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["APIBook/APIBook.csproj", "APIBook/"]
RUN dotnet restore "APIBook/APIBook.csproj"
COPY . .
WORKDIR "/src/APIBook"
RUN dotnet build "APIBook.csproj" -c Release -o app

FROM build AS publish
RUN dotnet publish "APIBook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIBook.dll"]