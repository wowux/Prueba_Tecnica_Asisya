# Backend
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["backend/src/Asisya.Api/Asisya.Api.csproj", "Asisya.Api/"]
COPY ["backend/src/Asisya.Application/Asisya.Application.csproj", "Asisya.Application/"]
COPY ["backend/src/Asisya.Domain/Asisya.Domain.csproj", "Asisya.Domain/"]
COPY ["backend/src/Asisya.Infrastructure/Asisya.Infrastructure.csproj", "Asisya.Infrastructure/"]
RUN dotnet restore "Asisya.Api/Asisya.Api.csproj"
COPY backend/src/ .
WORKDIR "/src/Asisya.Api"
RUN dotnet build "Asisya.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Asisya.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Asisya.Api.dll"]