# case-management-api-project

## Usage
Project folder containing C# .NET 8 solution file and API definition 
Contains EF Core Entities for SQL Server Database

## File Structure
Provide a visual representation of your project directories.
```bash
.
CaseManagement.Api.Tests/ # Solution file containing automated tests (used in CI/CD pipeline)
CaseManagement.Api/
│
├── Database/ # TSQL definitions of Relationi Database schema (explicit but not used)
├── Controllers/ #Exposed API endpoints 
│   ├── AuthController
│   ├── CaseController
│   ├── ClientController
│   ├── OfficerController
│   └── ServiceProviderController
│
├── Domain/ # Containing Domain definitions
│   ├── Enums # Defines Enums to map entity field values to system values
│   ├── DTOs # Defines Data Transfer Objects
│   └── Entities # Defines Entity records (for Repository)
│
├── Infrastructure/
│   ├── Auditing # Implements Interceptor service to audit changes to Entities
│   ├── Data 
│   │   ├── Configurations/ # Defines all EF Entities Configurations for DB            
│   │   ├── AppDbContext # Implements DBContext for EF Configs for .NET app using EF Core
│   │   └── DbInitializer # DB seeding static class
│   ├── Mappings # Mapster based mapping of objects in project (Used with Dapper and explicit TSQL approach, not in use)
│   ├── Repositories # Defines data-interfaces methods for entities using EF Core
│   └── Security # Implement all user data, hashing, and context + JWT token service
│
├── Middleware/ # Folder containing defined middleware processes
│   └── ExceptionMiddeware
│
├── Migrations/ # Containing migrations of Database schema 
│
├── appsettings.json # Main config file for app, stores application setting in JSON format 
│
├── Program.cs # C# .Net Program file
│
└── README.md 
