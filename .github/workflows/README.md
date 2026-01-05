## Github Actions - CI/CD Pipeline


## Deployment Highlights
Pipeline triggered by: 
- Successful merge on main 
- Opened pull request onto main 

4 Stages:
- Lint
- Build
- Test
- Deploy

Uses GitHub OIDC + Azure Federated Identity

Uses Azure Managed Identity for SQL access

Zero secrets in pipeline & uses GitHub Actions' enviroment based secrets

---

![Backend CI/CD Pipeline](../../docs/backend-cicd.png)