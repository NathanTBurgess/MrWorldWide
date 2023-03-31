FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MrWorldwide.WebApi/MrWorldwide.WebApi.csproj", "MrWorldwide.WebApi/"]
RUN dotnet restore "MrWorldwide.WebApi/MrWorldwide.WebApi.csproj"
COPY . .
WORKDIR "/src/MrWorldwide.WebApi"
RUN dotnet build "MrWorldwide.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MrWorldwide.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MrWorldwide.WebApi.dll"]
