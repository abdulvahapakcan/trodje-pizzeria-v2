# =========================
# BUILD
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY PizzeriaOrder.Api.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish PizzeriaOrder.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


# =========================
# RUNTIME
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 10000

ENTRYPOINT ["dotnet", "PizzeriaOrder.Api.dll"]