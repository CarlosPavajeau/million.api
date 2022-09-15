FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["apps/Million.Api/Million.Api.csproj", "Million.Api/"]
RUN dotnet restore "apps/Million.Api/Million.Api.csproj"
COPY . .
WORKDIR "/src/Million.Api"
RUN dotnet build "Million.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Million.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Million.Api.dll"]
