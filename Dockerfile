FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Restore as distinct layers
COPY EventsCEC.sln ./
COPY ./src/EventsCEC.App/EventsCEC.App.csproj ./src/EventsCEC.App/
COPY ./src/EventsCEC.Application/EventsCEC.Application.csproj ./src/EventsCEC.Application/
COPY ./src/EventsCEC.Domain/EventsCEC.Domain.csproj ./src/EventsCEC.Domain/
COPY ./src/EventsCEC.Infra.Data/EventsCEC.Infra.Data.csproj ./src/EventsCEC.Infra.Data/
COPY ./src/EventsCEC.Infra.IoC/EventsCEC.Infra.IoC.csproj ./src/EventsCEC.Infra.IoC/
RUN dotnet restore

# Build and publish a release
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80

ENTRYPOINT ["dotnet", "EventsCEC.App.dll"]