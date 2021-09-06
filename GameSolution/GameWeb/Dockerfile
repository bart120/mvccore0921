#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:5.0-buster AS build
WORKDIR /src
COPY ["GameWeb/GameWeb.csproj", "GameWeb/"]
RUN dotnet restore "GameWeb/GameWeb.csproj"
COPY . .
WORKDIR "/src/GameWeb"
RUN dotnet build "GameWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GameWeb.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GameWeb.dll"]
