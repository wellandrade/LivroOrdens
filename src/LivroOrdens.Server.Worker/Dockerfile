FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["LivroOrdens.slnx", "./"]
COPY ["src/LivroOrdens.Server.Worker/LivroOrdens.Server.Worker.csproj", "src/LivroOrdens.Server.Worker/"]
COPY ["src/LivroOrdens.Aplicacao/LivroOrdens.Aplicacao.csproj", "src/LivroOrdens.Aplicacao/"]
COPY ["src/LivroOrdens.Dominio/LivroOrdens.Dominio.csproj", "src/LivroOrdens.Dominio/"]
COPY ["src/LivroOrdens.Fix/LivroOrdens.Fix.csproj", "src/LivroOrdens.Fix/"]
COPY ["src/LivroOrdens.Infra/LivroOrdens.Infra.csproj", "src/LivroOrdens.Infra/"]

RUN dotnet restore "src/LivroOrdens.Server.Worker/LivroOrdens.Server.Worker.csproj"

COPY . .

RUN dotnet publish "src/LivroOrdens.Server.Worker/LivroOrdens.Server.Worker.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LivroOrdens.Server.Worker.dll"]