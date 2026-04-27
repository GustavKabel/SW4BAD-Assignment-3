FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /source

# copy csproj and restore
COPY MissionLogGenerator.csproj .
RUN dotnet restore MissionLogGenerator.csproj

# Copy everything else ad build
COPY . .
RUN dotnet publish MissionLogGenerator.csproj -c Release -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app -
ENTRYPOINT [ "dotnet", "MissionLogGenerator.dll"]