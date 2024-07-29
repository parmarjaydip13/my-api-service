FROM mcr.microsoft.com/dotnet/sdk:8.0 AS sdk
EXPOSE 5000
EXPOSE 5001

ARG Configuration=Release
ENV DOTNET_CLI_TELEMETRY_OPTOUT=true \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true \
    ASPNETCORE_URLS=http://+:8080
WORKDIR /src
COPY "my-api-service.sln" "."
COPY "src/TodoApplication.API/*.csproj" "src/TodoApplication.API/"
RUN dotnet restore
COPY . .

RUN dotnet build --configuration $Configuration
RUN dotnet test --configuration $Configuration --no-build
RUN dotnet publish "src/TodoApplication.API/TodoApplication.API.csproj" --configuration $Configuration --no-build --output /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
WORKDIR /app

COPY --from=sdk /app .

ENTRYPOINT ["dotnet", "TodoApplication.API.dll"]
