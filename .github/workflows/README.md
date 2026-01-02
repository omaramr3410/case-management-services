# Case-Management-Services 
## Github Actions  CI/CD Pipeline

flowchart LR
    GitHub[GitHub Push<br/>main]
    Actions[GitHub Actions]
    Azure[Azure App Service]
    DB[(Azure SQL)]

    GitHub --> Actions
    Actions --> Azure 
    Azure --> DB

## Deployment Highlights
Triggered on merge to main

4 Stages:
- Lint
- Build
- Test
- Deploy

Uses GitHub OIDC + Azure Federated Identity

Uses Azure Managed Identity for SQL access

Zero secrets in pipeline & uses GitHub Actions' enviroment based secrets