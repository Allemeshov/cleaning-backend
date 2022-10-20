FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ./chores.Web ./chores.Web
COPY ./chores.Infrastructure ./chores.Infrastructure
COPY ./chores.BLL ./chores.BLL
COPY ./chores.DAL ./chores.DAL
COPY ./chores.Web.Models ./chores.Web.Models

# restore only main project, it references everything that is required
RUN dotnet restore ./chores.Web/chores.Web.csproj

RUN dotnet publish ./chores.Web/chores.Web.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "chores.Web.dll"]