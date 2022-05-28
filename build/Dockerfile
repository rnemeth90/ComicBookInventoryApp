#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../src/ComicBookInventory.Api/ComicBookInventory.Api.csproj", "ComicBookInventory.Api/"]
COPY ["../src/ComicBookInventory.DataAccess.EFCore/ComicBookInventory.DataAccess.EFCore.csproj", "ComicBookInventory.DataAccess.EFCore/"]
COPY ["../src/ComicBookInventory.Domain/ComicBookInventory.Domain.csproj", "ComicBookInventory.Domain/"]
RUN dotnet restore "ComicBookInventory.Api/ComicBookInventory.Api.csproj"
COPY . .
WORKDIR "/src/ComicBookInventory.Api"
RUN dotnet build "ComicBookInventory.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ComicBookInventory.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComicBookInventory.Api.dll"]