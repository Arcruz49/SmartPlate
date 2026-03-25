# SmartPlate

API REST para acompanhamento nutricional e de dieta com análise de refeições por inteligência artificial.

## Sobre o projeto

SmartPlate é uma aplicação backend que ajuda no controle da alimentação e progresso físico. Permite registrar refeições (com foto ou código de barras), calcular macronutrientes automaticamente via IA, definir metas nutricionais personalizadas e acompanhar métricas corporais ao longo do tempo.

## Funcionalidades

- **Autenticação** — Cadastro e login com JWT
- **Perfil do usuário** — Dados físicos, objetivos, nível de atividade e rotina de treino
- **Registro de refeições**
  - Análise automática por imagem via Google Gemini
  - Busca de alimentos por código de barras (OpenFoodFacts)
  - Registro manual com macros customizados
- **Metas nutricionais** — Geradas por IA com base no perfil ou definidas manualmente (calorias, proteína, carboidratos, gordura)
- **Métricas de refeições** — Resumos diários, semanais e mensais
- **Métricas corporais** — Histórico de peso, % de gordura, massa muscular e medidas

## Stack

- **Runtime**: .NET 8 / ASP.NET Core
- **Banco de dados**: PostgreSQL + Entity Framework Core 8 (Npgsql)
- **Autenticação**: JWT Bearer (HS256)
- **IA**: Google Gemini (análise de imagem e geração de insights)
- **API externa**: OpenFoodFacts (consulta nutricional por EAN)
- **Documentação**: Swagger/OpenAPI

## Arquitetura

O projeto segue Clean Architecture com separação em camadas:

```
Domain/          → Entidades, enums e value objects
Application/     → Use cases, interfaces e DTOs
Infrastructure/  → Banco de dados, IA e APIs externas
Controllers/     → Endpoints HTTP
```

## Pré-requisitos

- .NET 8 SDK
- PostgreSQL 12+
- Chave de API do Google Gemini

## Configuração

**1. Banco de dados**

Crie o banco e execute o schema:

```bash
psql -U postgres -c "CREATE DATABASE smartplatedb;"
psql -U postgres -d smartplatedb -f sql.txt
```

**2. Variáveis de configuração**

Edite `appsettings.json` ou configure via variáveis de ambiente:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=smartplatedb;Username=postgres;Password=suasenha"
  },
  "Jwt": {
    "Key": "sua-chave-secreta-256-bits",
    "Issuer": "SmartPlateAPI",
    "Audience": "SmartPlateClient",
    "ExpirationMinutes": 1440
  },
  "GeminiAPI": {
    "ApiKey": "sua-api-key-gemini",
    "BaseUrl": "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent"
  }
}
```

> **Importante**: nunca suba chaves reais para o repositório. Use `dotnet user-secrets` em desenvolvimento ou variáveis de ambiente em produção.

**3. Executar**

```bash
dotnet restore
dotnet run
```

A API sobe em `http://localhost:5052` e `https://localhost:7052`.

O Swagger fica disponível em `/swagger` no ambiente de desenvolvimento.

## Endpoints

Todos os endpoints (exceto autenticação) exigem `Authorization: Bearer <token>`.

### Autenticação
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/auth/register` | Cadastro de usuário |
| POST | `/auth/login` | Login e geração de token |

### Perfil do usuário
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/user/userdata` | Criar/atualizar perfil |
| GET | `/user/userdata` | Buscar perfil |

### Metas nutricionais
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/userinsights/userinsights` | Gerar metas via IA |
| GET | `/userinsights/userinsights` | Buscar metas atuais |
| POST | `/userinsights/userinsights-rules` | Definir metas manualmente |

### Refeições
| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/usermeals/usermeal` | Registrar refeição (suporta imagem) |
| GET | `/usermeals/usermeal` | Listar refeições por data |
| GET | `/usermeals/usermealById` | Buscar refeição por ID |
| DELETE | `/usermeals/usermeal` | Deletar refeição |
| POST | `/usermeals/usermeal-rules` | Registrar refeição com macros manuais |
| GET | `/usermeals/usermeal-barcode` | Buscar alimento por código de barras |

### Métricas
| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/metrics/mealmetrics` | Métricas de refeições (diário/semanal/mensal) |
| GET | `/metrics/userbodymetrics` | Histórico de métricas corporais |

## Rate Limiting

- Login: 5 tentativas por minuto por IP
- Cadastro: 3 tentativas a cada 10 minutos por IP

## Licença

Projeto pessoal — uso livre.
