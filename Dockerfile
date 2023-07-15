FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY af.assessment.api/*.csproj ./af.assessment.api/
COPY af.assessment.api.test/*.csproj ./af.assessment.api.test/
COPY af.assessment.api.IntegrationTests/*.csproj ./af.assessment.api.IntegrationTests/
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /app/af.assessment.api
RUN dotnet publish -c Release -o out

FROM build as functionaltest
WORKDIR /app/af.assessment.api.IntegrationTests

FROM build as unittest
WORKDIR /app/af.assessment.api.test

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/af.assessment.api/out ./
ENTRYPOINT ["dotnet", "af.assessment.api.dll"]