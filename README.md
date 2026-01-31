# 📌 Project Management System — Backend

A scalable **N-Tier backend system** built using **ASP.NET Core Web API**, **Azure Functions**, **Entity Framework Core**, and **Dependency Injection**, following clean architecture principles.

This project demonstrates:
- Enterprise-grade layering (API → BLL → DAL → DB)
- Background processing using Azure Functions
- Proper dependency injection across projects
- Repository pattern with EF Core
- DTO mapping using AutoMapper
- Timer-triggered background jobs

---

## 🧱 Architecture Overview
```bash
Client (Angular / Postman)
↓
ASP.NET Core API
↓
Business Logic Layer (BLL)
↓
Data Access Layer (DAL)
↓
SQL Server Database
↓
Azure Functions (Background Jobs / Timers)
```
---

## 🚀 Recent Implementations

### ✅ Dependency Injection Across Layers
- Centralized service registration using extension methods
- Shared DI configuration between API and Azure Functions

### ✅ Azure Functions Integration
- Timer Trigger (`SendTaskNotification`)
- Injected BLL services into Azure Functions
- Configured isolated worker model

### ✅ EF Core + Repository Pattern
- Implemented:
  - `IGenericRepository<T>`
  - `ITaskRepository`, `IPersonRepository`, `IProjectRepository`, etc.
- SQL Server backed persistence

### ✅ AutoMapper Integration
- Centralized mapping profiles
- Clean separation between entities and DTOs

### ✅ Strong Typing with Enums
- `RoleEnum`
- `TaskStatusEnum`

### ✅ JWT Authentication & Authorization
- Bearer token authentication
- Role-based authorization support

### ✅ Azure Storage Emulator (Azurite)
- Local Azure Functions runtime storage support

---

## ⚙️ Tech Stack

| Area | Technology |
|------|------------|
| Language | C# (.NET 8/9/10 isolated worker) |
| API Framework | ASP.NET Core Web API |
| Background Jobs | Azure Functions (Timer Trigger) |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Mapping | AutoMapper |
| Validation | FluentValidation |
| Authentication | JWT Bearer |
| DI Container | Microsoft.Extensions.DependencyInjection |
| Local Storage Emulator | Azurite |

---

## 🔧 Setup Instructions
```bash

🔁 ProjectManagement.sln
│
├── ProjectManagement.API        # REST API layer
├── ProjectManagement.BLL        # Business logic layer
├── ProjectManagement.DAL        # Data access layer
├── ProjectManagement.Common     # DTOs, Enums, Shared Models
├── ProjectManagement.Functions  # Azure Functions (Timer / Background Jobs)
└── ProjectManagement.Tests      # (Optional) Unit tests
```
## 1️⃣ Clone Repository
```bash
git clone https://github.com/gowthambsoftsuave-pixel/project-management-system.git
cd project-management-system
```
## 2️⃣ Configure Database
Update appsettings.json in API and Functions projects:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ProjectManagementDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```
```bash
Run migrations:
dotnet ef database update
```
## 3️⃣ Install Azure Functions Core Tools
```bash
npm install -g azure-functions-core-tools@4 --unsafe-perm true
```
## 4️⃣ Start Azurite (Required for Timer Triggers)
```bash
azurite
```
Timer triggers require Azure Storage to track execution state. Without Azurite, Functions will fail to start.
## 5️⃣ Run API
```bash
dotnet run --project ProjectManagement.API
```
API will start at:https://localhost:5001
## 6️⃣ Run Azure Functions
```bash
cd ProjectManagement.Functions
func start
```
Timer trigger will activate automatically based on schedule.


## 🔁 Dependency Injection Flow
```bash
API / Azure Functions
        ↓
    BLL Services
        ↓
    DAL Repositories
        ↓
      DbContext
        ↓
     SQL Server
```
