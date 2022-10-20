FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# everything is prefixed with the dot, so it's contained in /app

# copy dependencies as-is, don't change paths
COPY ./chores.Infrastructure ./chores.Infrastructure
COPY ./chores.BLL ./chores.BLL
COPY ./chores.DAL ./chores.DAL
COPY ./chores.Web.Models ./chores.Web.Models

# copy main project
COPY ./WebRtc.Call.Web ./WebRtc.Call.Web

# restore only main project, it references everything that is required
RUN dotnet restore ./WebRtc.Call.Web/WebRtc.Call.Web.csproj


RUN dotnet publish ./WebRtc.Call.Web/WebRtc.Call.Web.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# publish runs from /app (./), so it outputs to /app/out
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebRtc.Call.Web.dll"]
