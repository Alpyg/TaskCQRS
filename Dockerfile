FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TaskCQRS.slnx .
COPY TaskCQRS.Api/TaskCQRS.Api.csproj TaskCQRS.Api/
COPY TaskCQRS.Application/TaskCQRS.Application.csproj TaskCQRS.Application/
COPY TaskCQRS.Domain/TaskCQRS.Domain.csproj TaskCQRS.Domain/
COPY TaskCQRS.Infrastructure/TaskCQRS.Infrastructure.csproj TaskCQRS.Infrastructure/

RUN dotnet restore

COPY . .

RUN dotnet publish TaskCQRS.Api/TaskCQRS.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TaskCQRS.Api.dll"]
