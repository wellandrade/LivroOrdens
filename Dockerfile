FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["LivroOrdens.slnx", "./"]
COPY ["src/LivroOrdens.API/LivroOrdens.API.csproj", "src/LivroOrdens.API/"]
COPY ["src/LivroOrdens.Aplicacao/LivroOrdens.Aplicacao.csproj", "src/LivroOrdens.Aplicacao/"]
COPY ["src/LivroOrdens.Dominio/LivroOrdens.Dominio.csproj", "src/LivroOrdens.Dominio/"]
COPY ["src/LivroOrdens.Fix/LivroOrdens.Fix.csproj", "src/LivroOrdens.Fix/"]
COPY ["src/LivroOrdens.Infra/LivroOrdens.Infra.csproj", "src/LivroOrdens.Infra/"]

RUN dotnet restore "src/LivroOrdens.API/LivroOrdens.API.csproj"

COPY . .

RUN dotnet publish "src/LivroOrdens.API/LivroOrdens.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "LivroOrdens.API.dll"]