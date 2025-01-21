FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Restore as distinct layers
COPY EventsCEC.sln ./
COPY ./EventsCEC.App/EventsCEC.App.csproj ./EventsCEC.App/
COPY ./EventsCEC.Application/EventsCEC.Application.csproj ./EventsCEC.Application/
COPY ./EventsCEC.Domain/EventsCEC.Domain.csproj ./EventsCEC.Domain/
COPY ./EventsCEC.Infra.Data/EventsCEC.Infra.Data.csproj ./EventsCEC.Infra.Data/
COPY ./EventsCEC.Infra.IoC/EventsCEC.Infra.IoC.csproj ./EventsCEC.Infra.IoC/
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