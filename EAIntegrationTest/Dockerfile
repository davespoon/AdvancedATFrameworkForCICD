#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["EAIntegrationTest/EAIntegrationTest.csproj", "EAIntegrationTest/"]
COPY ["ProductAPI/ProductAPI.csproj", "ProductAPI/"]
RUN dotnet restore "EAIntegrationTest/EAIntegrationTest.csproj"
COPY . .
WORKDIR "/src/EAIntegrationTest"
RUN dotnet build "EAIntegrationTest.csproj" -c Release -o /app/build


FROM build AS EAIntegration
WORKDIR /src/EAIntegrationTest
CMD ["dotnet", "test"]