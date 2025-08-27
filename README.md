# 📊 Simulador de Empréstimos - Minimal API (.NET 8)

API desenvolvida em **C# (.NET 8)** utilizando o modelo **Minimal API** e arquitetura **Clean Architecture simplificada**, com foco em simulação de empréstimos. A aplicação recebe solicitações via JSON, realiza validações e cálculos de amortização (SAC e PRICE), persiste os dados e envia eventos para o EventHub.

---

## 🧱 Arquitetura

O projeto está organizado em quatro camadas principais:

- **Api**: Camada de aplicação, responsável pelos endpoints e orquestração.
- **Core**: Regras de negócio e entidades.
- **Infra**: Acesso a recursos externos como bancos de dados.
- **Mensageria**: Adapter para envio de eventos ao **Azure EventHub**.

### Estrutura de referencia 
```
SimuladorDeEmprestimos/
├── .devcontainer/
│   └── devcontainer.json
├── README.md
├── .dockerignore
├── .gitignore
├── docker-compose.yml
├── Dockerfile
├── appsettings.json
├── appsettings.Development.json
├── SimuladorDeEmprestimos.sln
└── src/
    └── Simulador/
        ├── Api/
        │   ├── Program.cs
        │   ├── Endpoints/
        │   ├── Services/
        │   ├── Dtos/
        │   ├── Validators/
        │   └── Telemetria/
        ├── Core/
        │   ├── Entities/
        │   ├── ValueObjects/
        │   ├── Interfaces/
        │   └── UseCases/
        ├── Infra/
        │   ├── Context/
        │   ├── Repositories/
        │   ├── Migrations/
        │   └── ExternalServices/
        └── Mensageria/
            ├── EventHub/
            └── Adapters/
```

## 🗄️ Banco de Dados

- **SQL Server (nuvem)**: Utilizado para consultar os parâmetros dos produtos.
- **PostgreSQL (local)**: Utilizado para persistir as simulações realizadas.

> ⚠️ **Antes de iniciar a aplicação, é necessário executar as migrations para criar o banco local:**

```bash
dotnet ef database update --project src/Infra --startup-project src/Api
```

---

## 🐳 Execução com Docker e Dev Container

### Requisitos

- .NET 8 SDK
- Docker
- Docker Compose
- VS Code com extensão **Dev Containers**

### Execução

```bash
docker-compose up --build
```

> O `docker-compose.yml` inclui os serviços da API e do banco PostgreSQL. O SQL Server é acessado externamente (nuvem).

### Dev Container

- Abra o projeto no VS Code.
- Selecione **"Reabrir no Container"**.
- Aguarde a instalação das dependências e inicialização do ambiente.

---

## 📡 Mensageria

- Envia eventos de simulação para o **Azure EventHub**, simulando integração com a área de relacionamento da empresa.
- O evento contém o envelope JSON da simulação.

---

## 📈 Telemetria

- Registra:
  - Tempo de resposta por serviço.
  - Quantidade de simulações realizadas.

---

## 📦 Endpoints

Grupo principal: `/simulador`

| Método | Rota                           | Descrição                                                                 |
|--------|--------------------------------|---------------------------------------------------------------------------|
| POST   | `/simulador/simular`           | Recebe envelope JSON e retorna simulação SAC e PRICE                      |
| GET    | `/simulador/realizadas`        | Lista todas as simulações realizadas                                     |
| GET    | `/simulador/minhas-simulacoes` | Retorna simulações do cliente atual (exemplo estático)                   |
| GET    | `/simulador/diarias-produtos`  | Retorna valores simulados por produto e por dia                          |
| GET    | `/telemetria`                  | Retorna métricas de tempo e volume de simulações                         |

> Todas as rotas estão documentadas e disponíveis para teste via **Swagger UI**, acessível ao iniciar a aplicação.

---
---

## ⚙️ Configuração via `appsettings.json`

A aplicação utiliza um arquivo `appsettings.json` com a seguinte estrutura básica:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "ProdutosConnection": "<string de conexão para SQL Server>",
    "SimulacaoConnection": "<string de conexão para PostgreSQL>"
  },
  "AzureEventHub": {
    "ConnectionString": "<string de conexão do EventHub>",
    "HubName": "<nome do hub>"
  }
}
```

> ⚠️ **Importante:** Em ambientes de produção, recomenda-se utilizar mecanismos mais seguros para gerenciamento de configurações sensíveis, como:
- Variáveis de ambiente
- Azure Key Vault
- AWS Secrets Manager
- Arquivos protegidos por criptografia

---
## 📚 Referências

- O que é API
- Calculadora SAC e PRICE
- Azure EventHub
