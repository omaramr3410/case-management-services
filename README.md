# case-management-services

Backend Project for CMS, programmed in .NET 8

![CI/CD](https://github.com/omaramr3410/case-management-services/actions/workflows/deploy-api.yml/badge.svg)

flowchart TB
Client[Frontend / API Consumer]

    Client -->|HTTP + JWT| Controller

    subgraph API Layer
        Controller[Controllers]
    end

    Controller --> Orchestrator

    subgraph Application Layer
        Orchestrator[Orchestrators]
        AuditService[Audit Service]
        UserContext[User Context]
    end

    Orchestrator --> Repository
    Orchestrator --> AuditService
    AuditService --> Repository

    subgraph Data Access Layer
        Repository[Repositories]
    end

    Repository --> SQL[(SQL Server)]

Migrations CLI:
dotnet ef migrations add InitialCreate --project CaseManagement.Api --startup-project CaseManagement.Api

DB Update CLI:
dotnet ef database update --project CaseManagement.Api --startup-project CaseManagement.Api

--Future schema changes--

Update entity/configuration classes → generate a new migration:

dotnet ef migrations add AddNewFieldToClient
dotnet ef database update
