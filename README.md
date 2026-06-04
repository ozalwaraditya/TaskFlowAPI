# TaskFlowAPI

A Task Management Web API built with **.NET 8**, demonstrating real-world backend patterns like authentication, authorization, clean architecture, repository & service layers, custom middleware, async/await, EF Core, and structured logging.

This project was built as a learning/demo project to strengthen skills in .NET Core Web API development.

---

## Features

✅ JWT Authentication & Role-based Authorization  
✅ Task CRUD Operations (Create, Read, Update, Delete, Assign, StatusChange)  
✅ User Management (Register, Login)  
✅ Clean Architecture (Controller → Service → Repository → DbContext)  
✅ Repository Pattern & Service Layer  
✅ Custom Middleware (Global Exception Handling, Correlation-Id)  
✅ Async/Await throughout the entire call chain  
✅ Dependency Injection (built-in .NET DI)  
✅ Serilog Logging (Console + File)  
✅ Swagger / OpenAPI Documentation  

---

## Tech Stack

- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core (Code-First + SQL Server)
- JWT Bearer Authentication
- Serilog for structured logging
- Swagger / OpenAPI

---

## Project Structure

```
TaskFlowAPI/
├── Controllers/       # API endpoints
├── Services/          # Business logic
├── Repositories/      # Data access abstraction
├── Models/            # Domain entities
├── DTOs/              # Request / Response contracts
├── Middleware/        # Custom exception handling middleware
├── Extensions/        # DI registration helpers
└── Data/
    └── AppDbContext   # EF Core DbContext
```

---

## Database Schema

**Users**
- UserId (PK)
- Username
- Email
- HashedPassword
- Role (Admin / User)
- CreatedAt

**Tasks**
- TaskId (PK)
- Title
- Description
- AssignedToUserId (FK → Users)
- CreatedByUserId (FK → Users)
- Status
- Priority
- DueDate
- CreatedAt / UpdatedAt

---

## Getting Started

**1. Clone the repo**
```bash
git clone https://github.com/ozalwaraditya/TaskFlowAPI.git
cd TaskFlowAPI
```

**2. Configure the database**

Update `appsettings.json` with your SQL Server connection string:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskFlowDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**3. Apply migrations & run**
```bash
dotnet ef database update
dotnet run
```

API will be available at:
- `https://localhost:{port}/swagger`

---

## API Examples

**1. Login**
```
POST /api/auth/login
Body: { "username": "", "password": "" }
```

**2. Get all tasks**
```
GET /api/tasks
Headers: Authorization: Bearer <token>
```

**3. Create a task**
```
POST /api/tasks
Headers: Authorization: Bearer <token>
Body:
{
  "title": "Fix login bug",
  "description": "Investigate auth token expiry",
  "dueDate": "2025-12-31T23:59:59",
  "priority": "High"
}
```

## What I Learned

This project helped me practice:
- Structuring a Web API using Clean Architecture
- Implementing JWT authentication and role-based authorization
- Applying the Repository and Service layer patterns
- Writing custom middleware for global exception handling
- Using Dependency Injection to keep components loosely coupled
- Async/await best practices across all layers

---

## Next Steps (Future Improvements)

- Add unit & integration tests (xUnit + Moq)
- Dockerize the API
- Add pagination, filtering & sorting for task listing
- Implement refresh tokens
- Add CI/CD pipeline with GitHub Actions

---

## How to Use This Repo

Feel free to fork and use this as a reference or starter for building your own .NET APIs.  
If you find it useful, ⭐ it on GitHub and connect with me on [LinkedIn](https://www.linkedin.com/in/aditya-ozalwar-530283291/).
