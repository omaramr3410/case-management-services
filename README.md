# case-management-services

Backend Project for CMS, programmed in .NET 8

![CI/CD](https://github.com/omaramr3410/case-management-services/actions/workflows/deploy-api.yml/badge.svg)

# Case Management System – Backend Services

This repository contains the backend API for a lightweight Case Management System designed to demonstrate secure, cloud-native backend architecture using ASP.NET Core and Azure.

The system supports role-based access control, secure authentication, automated database migrations, and CI/CD deployment.

Domain: 
- User entity manages user info for login state, etc
- Case entity defines the unit of work connecting Client and Officer 
- Officer entity defines an individual capable of working on a Case to successfully resolve
- Client entity defines a person(s), company, or group that initiates/opens a Case
- ServiceProvider entity defines a distinct company/service managed by the CMS

---

## 🧱 Tech Stack

- **ASP.NET Core (.NET 8)**
- **Entity Framework Core**
- **Azure SQL Database**
- **Azure App Service (Windows)**
- **GitHub Actions (CI/CD)**
- **Azure Managed Identity**
- **Swagger / OpenAPI**

---

## 🎯 Core Features

- Role-based access control (**Admin**, **Reviewer**)
- Secure JWT authentication
- Case creation and review workflows
- Health monitoring endpoint
- Swagger API documentation
- Automatic database migrations in Production
- No secrets stored in source control or CI/CD
- Health Monitoring via /health
- Swagger UI available at /swagger

---

## 🏗️ System Architecture

```mermaid
flowchart LR
    User[End User<br/>(Admin / Reviewer)]
    UI[Angular UI]
    API[ASP.NET Core API]
    DB[(Azure SQL Database)]

    User -->|HTTPS| UI
    UI -->|JWT| API
    API -->|Managed Identity| DB
```

---

## 🏗️ Authentication & Authorization

- Users authenticate using JWT tokens

- Role-based authorization enforced at controller level
- Example: Only create case by Admin, Manager, Officer
```mermaid
[Authorize(Roles = "Admin, Manager, Officer")]
public IActionResult CreateCase() { ... }
```


- Supported roles:

```mermaid
Admin – Full Admin access

Manager - Manages Officers and access to all subordinates cases 

Officer - Case Officer

Reviewer – limited; only review permissions for Auditor
```
---

**Operational Guidance**

Migrations CLI:
dotnet ef migrations add InitialCreate --project CaseManagement.Api --startup-project CaseManagement.Api

DB Update CLI:
dotnet ef database update --project CaseManagement.Api --startup-project CaseManagement.Api

--Future schema changes--

Update entity/configuration classes → generate a new migration:

dotnet ef migrations add AddNewFieldToClient

dotnet ef database update

