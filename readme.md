# Livro de Ordens - FIX Protocolo

## Sobre o projeto
Projeto desenvolvido em .NET para simular um Livro de Ordens utilizando o protocolo FIX.
A solução foi desenvolvida seguindo princípios de Clean Architecture, SOLID e boas práticas de desenvolvimento, mantendo as responsabilidades separadas entre Domínio, Aplicação, Infraestrutura e API.

## Tecnologias
- .NET 10
- ASP.NET Core Web API
- QuickFIX/n
- xUnit
- Docker
- Docker Compose

## Arquitetura
A solução está organizada da seguinte forma:

```
src
├── LivroOrdens.API
├── LivroOrdens.Aplicacao
├── LivroOrdens.Dominio
├── LivroOrdens.Infra
├── LivroOrdens.Cliente
└── LivroOrdens.Fix

tests
└── LivroOrdens.Tests
```

### Responsabilidade de cada projeto
| Projeto | Responsabilidade |
| LivroOrdens.API | Exposição dos endpoints REST |
| LivroOrdens.Aplicacao | Casos de uso da aplicação |
| LivroOrdens.Dominio | Regras de negócio e entidades |
| LivroOrdens.Infra | Repositórios e infraestrutura |
| LivroOrdens.Cliente | Cliente FIX responsável pelo envio das mensagens |
| LivroOrdens.Fix | Servidor FIX responsável pelo processamento das mensagens |
| LivroOrdens.Tests | Testes unitários |

---
## Funcionalidades
- Cadastro de Ordens
- Cancelamento de Ordens
- Consulta ao Livro de Ordens
- Comunicação utilizando protocolo FIX
- Validações de Domínio
- Testes Unitários
- Docker
---

## Endpoints
### Criar Ordem
```
POST /api/ordens
```
### Cancelar Ordem
```
POST /api/ordens/cancelar
```
### Consultar Livro
```
GET /api/ordens/livro
```
---

## Regras implementadas
- Apenas os ativos PETR4 e VALE3 são aceitos.
- Quantidade deve ser maior que zero.
- Quantidade máxima permitida: 100000.
- Preço deve ser maior que zero.
- Preço máximo permitido conforme especificação.
- ClOrdId deve ser único.
---

# Executando o projeto
## Pré-requisitos
- .NET SDK 10
- Docker Desktop
---

## Executando localmente
Restaurar os pacotes

```bash
dotnet restore
```
COMPILAR
```bash
dotnet build
```
EXECUTAR
```bash
dotnet run --project src/LivroOrdens.API
```
---

## Executando utilizando Docker
CONSTRUTIR IMAGEM
```bash
docker compose build
```
SUBIR OS CONTAINERS

```bash
docker compose up
```
OU ENTAO DIRETO 

```bash
docker compose up --build
```

APÓS ISSO A API ESTARÁ DISPONIVEL EM 
```
http://localhost:8080/scalar
```
---
## Executando os testes
```bash
dotnet test
```
---

## Testes implementados
### Domínio
- Criação de ordem válida
- Símbolo inválido
- Símbolo vazio
- Quantidade inválida
- Preço inválido
### Casos de Uso
- Criação de ordem com sucesso
- Símbolo inválido
- Quantidade inválida
- Preço inválido
---
## Boas práticas utilizadas
- Clean Architecture
- SOLID
- Dependency Injection
- Repository Pattern
- Middleware para tratamento de requisições
- Validação utilizando Data Annotations
- Testes Unitários
- Dockerização da aplicação
