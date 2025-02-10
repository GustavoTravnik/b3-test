FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /server
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /server
COPY ["FinanceServices.csproj", "."]
RUN dotnet restore "./FinanceServices.csproj"
COPY . .
WORKDIR "/server/."
RUN dotnet build "./FinanceServices.csproj" -c $BUILD_CONFIGURATION -o /server/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FinanceServices.csproj" -c $BUILD_CONFIGURATION -o /server/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /server
COPY --from=publish /server/publish .
ENTRYPOINT ["dotnet", "FinanceServices.dll"]