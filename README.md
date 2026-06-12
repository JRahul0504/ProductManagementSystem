# Product Management System API

## Overview

Product Management System API is a production-ready RESTful Web API built using .NET 8 and Clean Architecture principles.

The solution provides secure Product and Item management with JWT Authentication, Refresh Tokens, Role-Based Authorization, Validation, Logging, Docker support, and automated testing.

The project follows enterprise software development practices with scalability, maintainability, security, and testability in mind.

---

# Tech Stack

| Category         | Technology               |
| ---------------- | ------------------------ |
| Framework        | .NET 8                   |
| API              | ASP.NET Core Web API     |
| Architecture     | Clean Architecture       |
| Database         | SQL Server               |
| ORM              | Entity Framework Core    |
| Authentication   | JWT Authentication       |
| Authorization    | Role-Based Authorization |
| Validation       | FluentValidation         |
| Object Mapping   | AutoMapper               |
| Logging          | Serilog                  |
| Documentation    | Swagger / OpenAPI        |
| Testing          | xUnit, Moq               |
| Containerization | Docker                   |
| CI/CD            | GitHub Actions           |

---

# Features

## Authentication

* User Registration
* User Login
* JWT Access Tokens
* Refresh Tokens
* Token Rotation
* Password Hashing
* Role-Based Authorization

## Product Management

* Create Product
* Update Product
* Delete Product
* Get Product By Id
* Get All Products
* Pagination
* Search
* Sorting

## Item Management

* Create Item
* Update Item
* Delete Item
* Get Items By Product

## Additional Features

* Global Exception Handling
* Request Logging
* Validation
* Swagger Documentation
* Health Checks
* Response Compression
* Rate Limiting
* Docker Support

---

# Architecture

```text
Client
   |
   v
Controllers
   |
   v
Application Layer
   |
   v
Domain Layer
   |
   v
Infrastructure Layer
   |
   v
SQL Server Database
```

## Layers

### API Layer

Responsibilities:

* Controllers
* Middleware
* Authentication
* Swagger Configuration

### Application Layer

Responsibilities:

* DTOs
* Interfaces
* Business Logic
* Validation Rules

### Domain Layer

Responsibilities:

* Entities
* Domain Models
* Domain Exceptions

### Infrastructure Layer

Responsibilities:

* Entity Framework Core
* Repositories
* Unit Of Work
* JWT Services
* Database Access

---

# Project Structure

```text
ProductManagementSystem

├── Dockerfile
├── docker-compose.yml
├── ProductManagementSystem.sln
├── README.md

├── src
│   ├── ProductManagementSystem.API
│   ├── ProductManagementSystem.Application
│   ├── ProductManagementSystem.Domain
│   └── ProductManagementSystem.Infrastructure

└── tests
    ├── ProductManagementSystem.Application.Tests
    └── ProductManagementSystem.Infrastructure.Tests
```

---

# Running Locally

## Clone Repository

```bash
git clone https://github.com/JRahul0504/ProductManagementSystem.git
cd ProductManagementSystem
```

## Restore Packages

```bash
dotnet restore
```

## Build Solution

```bash
dotnet build
```

## Apply Database Migration

```bash
dotnet ef database update --project src/ProductManagementSystem.Infrastructure --startup-project src/ProductManagementSystem.API
```

## Run Application

```bash
dotnet run --project src/ProductManagementSystem.API
```

---

# Running with Docker

## Build Containers

```bash
docker compose build
```

## Start Containers

```bash
docker compose up -d
```

## Verify Containers

```bash
docker ps
```

Expected Containers:

```text
product-api
product-db
```

## Open Swagger

```text
http://localhost:8080/swagger
```

---

# Authentication Flow

## Register User

```http
POST /api/auth/register
```

## Login User

```http
POST /api/auth/login
```

Login returns:

```json
{
  "accessToken": "jwt-token",
  "refreshToken": "refresh-token"
}
```

## Authorize

Click the Authorize button in Swagger and enter:

```text
Bearer YOUR_ACCESS_TOKEN
```

## Access Protected APIs

Use the JWT token to access secured Product and Item endpoints.

---

# Testing

Run all tests:

```bash
dotnet test
```

---

# Logging

The application uses Serilog for:

* Request Logging
* Error Logging
* Authentication Events
* Application Events

---

# Security

* JWT Authentication
* Refresh Tokens
* Password Hashing
* Authorization Policies
* Input Validation
* SQL Injection Protection through Entity Framework Core

---

# Docker Commands

## Start

```bash
docker compose up -d
```

## Stop

```bash
docker compose down
```

## Rebuild

```bash
docker compose up -d --build
```

## View Logs

```bash
docker logs product-api
```

---

# Git Workflow

After making changes:

```bash
git add .
git commit -m "Describe your changes"
git push
```

If Docker is being used:

```bash
docker compose up -d --build
```

to rebuild the application container with the latest code changes.

---

# CI/CD

GitHub Actions automatically:

* Restore Packages
* Build Solution
* Run Unit Tests

on every push to the main branch.

---

# Future Enhancements

* Soft Delete
* API Versioning
* Redis Caching
* CQRS Pattern
* MediatR
* RabbitMQ Integration
* Azure Deployment
* Kubernetes Deployment

---

# Author

Rahul Jatve

GitHub:
https://github.com/JRahul0504
