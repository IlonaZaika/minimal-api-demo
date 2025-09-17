# .NET 9 Minimal API Demo 🚀

[![.NET](https://img.shields.io/badge/.NET-9.0-purple?logo=dotnet)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EntityFrameworkCore-9.0.9-green)](https://learn.microsoft.com/en-us/ef/core/)
[![Docker](https://img.shields.io/badge/Docker-SQL--Server-blue?logo=docker)](https://hub.docker.com/_/microsoft-mssql-server)

A **demo REST API** built with:

- **.NET 9 Minimal APIs**
- **Entity Framework Core 9**
- **SQL Server 2022 in Docker**
- Full CRUD with pagination, validation & optimistic concurrency

👉 Repository: [minimal-api-demo](https://github.com/IlonaZaika/minimal-api-demo.git)

This project was created for **interview preparation** to demonstrate:

- Clean architecture with services, contracts & domain entities
- EF Core migrations workflow
- Optimistic concurrency using `RowVersion`
- Docker-based development with SQL Server

---

## 🛠️ Tech stack

- .NET 9.0.304
- Entity Framework Core 9.0.9
- SQL Server 2022 (Docker)
- Swagger (OpenAPI)

---

## ⚡ Getting started

### 1. Clone the repo

```bash
git clone https://github.com/IlonaZaika/minimal-api-demo.git
cd minimal-api-demo/Api
```

### 2. Run SQL Server in Docker

```bash
docker compose up -d
```

Default connection string (for demo purposes):

```pgsql
Server=localhost,1433;Database=DemoDb;User Id=sa;Password=#2Pencil;TrustServerCertificate=True
```

### 3. Apply migrations

```bash
dotnet ef database update
```

### 4. Run the API

```bash
dotnet run
```

👉 Swagger UI: http://localhost:[port]/swagger

### 📌 Example endpoints

Create product

```http
POST /api/products
{
  "name": "Laptop",
  "stock": 10,
  "price": 1200
}
```

Get products with pagination

```http
GET /api/products?page=1&pageSize=10
```

Update product

```http
PUT /api/products/1
{
  "id": 1,
  "name": "Updated Laptop",
  "stock": 12,
  "price": 1100
}
```

Delete product

```http
DELETE /api/products/1
```

### 🧩 Architecture

```root
Api/
 ├── Program.cs
 ├── Data/
 │    └── AppDbContext.cs
 ├── Domain/
 │    └── Product.cs
 ├── Contracts/
 │    └── IProductService.cs
 ├── Services/
 │    └── ProductService.cs
 ├── Migrations/
docker-compose.yml
```

- Domain → entity models
- Data → EF Core DbContext
- Contracts → interfaces (abstractions)
- Services → business logic implementations
- Api → endpoints (Minimal API)

### ✅ Features

- Clean separation of concerns (Contracts + Services)
- Database migrations with EF Core
- Dockerized SQL Server for easy setup
- Optimistic concurrency handling (RowVersion)
- Swagger documentation
- Pagination with Skip / Take

### 📖 Notes

- Default seed data is added automatically on first run.
- This project is for demo/interview purposes, not production.

### 🎯 Topics covered

- Differences between async/await in service methods
- How EF Core compiles async methods into state machines
- Entity Framework Core migrations workflow
- Minimal API vs MVC Controllers in .NET
- Handling concurrency with RowVersion in SQL Server
