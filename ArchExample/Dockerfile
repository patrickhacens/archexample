#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ArchExample/ArchExample.csproj", "ArchExample/"]
RUN dotnet restore "ArchExample/ArchExample.csproj"
COPY . .
WORKDIR "/src/ArchExample"
RUN dotnet build "ArchExample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArchExample.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchExample.dll"]