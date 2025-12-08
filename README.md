# PharmaGO API

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15-316192?logo=postgresql)](https://www.postgresql.org/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

> A modern, scalable REST API for pharmacy delivery and e-commerce operations built with Clean Architecture principles.

## 📋 Table of Contents

- [About The Project](#about-the-project)
- [Architecture & Design Patterns](#architecture--design-patterns)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Testing](#testing)
- [Key Features](#key-features)
- [Best Practices Implemented](#best-practices-implemented)

## 🎯 About The Project

PharmaGO API is a robust backend service designed for pharmacy delivery systems. The project demonstrates professional software engineering practices including Domain-Driven Design (DDD), Clean Architecture, Test-Driven Development (TDD), and SOLID principles using C# with .NET 10.0.

### Core Functionalities

- 🔐 **Authentication & Authorization** - JWT-based secure authentication
- 💊 **Product Management** - CRUD operations for pharmaceutical products
- 🏥 **Pharmacy Management** - Multi-pharmacy support system
- 👥 **Client Management** - User registration and profile management
- 🔍 **API Documentation** - Interactive API documentation with Scalar

## 🏗️ Architecture & Design Patterns

### Clean Architecture

The project follows Clean Architecture principles with clear separation of concerns across four main layers:

```
┌─────────────────────────────────────────┐
│         PharmaGO.Api (Presentation)     │
│   Controllers, Middleware, Mapping      │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│      PharmaGO.Application (Use Cases)   │
│    Commands, Queries, Validators        │
└─────────────────────────────────────────┘
                    ↓
┌─────────────────────────────────────────┐
│      PharmaGO.Core (Domain/Business)    │
│    Entities, Interfaces, Errors         │
└─────────────────────────────────────────┘
                    ↑
┌─────────────────────────────────────────┐
│   PharmaGO.Infrastructure (Data/External)│
│   Repositories, DbContext, JWT, Services│
└─────────────────────────────────────────┘
```

### Design Patterns Implemented

#### 1. **CQRS (Command Query Responsibility Segregation)**
- **Commands**: Handle write operations (Create, Update, Delete)
- **Queries**: Handle read operations (Get, List)
- Implemented using **MediatR** for clean command/query handling

#### 2. **Repository Pattern**
- Abstracts data access logic
- Interfaces defined in `Core` layer
- Implementations in `Infrastructure` layer
- Enables easy testing and swapping of data sources

#### 3. **Dependency Injection**
- Each layer registers its dependencies via extension methods
- Clean, maintainable service registration
- Follows Dependency Inversion Principle

#### 4. **ErrorOr Pattern**
- Functional error handling without exceptions
- Returns `ErrorOr<T>` for explicit error handling
- Better error visibility and handling at compile time

#### 5. **Pipeline Behavior Pattern**
- Cross-cutting concerns handled via MediatR behaviors
- Validation behavior automatically validates all commands
- Extensible for logging, caching, etc.

#### 6. **Options Pattern**
- Configuration strongly-typed using Options pattern
- JWT settings, connection strings configured via `appsettings.json`

## 🛠️ Tech Stack

### Core Technologies

| Technology | Purpose |
|-----------|---------|
| **.NET 10.0** | Runtime framework |
| **C# 13** | Programming language |
| **ASP.NET Core** | Web framework |
| **Entity Framework Core 10** | ORM |
| **PostgreSQL** | Database |

### Libraries & Packages

#### Application Layer
- **MediatR** (v14.0.0) - CQRS implementation
- **FluentValidation** (v12.1.1) - Input validation
- **BCrypt.Net-Core** (v1.6.0) - Password hashing

#### API Layer
- **Mapster** (v9.0.0) - Object mapping
- **Scalar.AspNetCore** (v2.11.1) - API documentation
- **Microsoft.AspNetCore.OpenApi** - OpenAPI support

#### Infrastructure Layer
- **Npgsql.EntityFrameworkCore.PostgreSQL** (v10.0.0) - PostgreSQL provider
- **System.IdentityModel.Tokens.Jwt** (v8.15.0) - JWT generation

#### Testing
- **xUnit** (v2.9.3) - Testing framework
- **Moq** (v4.20.72) - Mocking framework
- **FluentAssertions** (v8.8.0) - Assertion library

## 📁 Project Structure

```
pharmago.server.api/
├── src/
│   ├── PharmaGO.Api/                    # Presentation Layer
│   │   ├── Controllers/                 # API Controllers
│   │   ├── Errors/                      # Error handling
│   │   ├── Http/                        # HTTP context helpers
│   │   ├── Mapping/                     # DTO mappings
│   │   └── Program.cs                   # Application entry point
│   │
│   ├── PharmaGO.Application/            # Application Layer
│   │   ├── Authentication/              # Auth use cases
│   │   │   ├── Commands/                # Auth commands
│   │   │   ├── Queries/                 # Auth queries
│   │   │   └── Common/                  # Shared auth models
│   │   ├── Pharmacies/                  # Pharmacy use cases
│   │   ├── Products/                    # Product use cases
│   │   └── Common/
│   │       └── Behaviors/               # MediatR behaviors (validation)
│   │
│   ├── PharmaGO.Core/                   # Domain Layer
│   │   ├── Entities/                    # Domain entities
│   │   │   ├── Base/                    # Base entity classes
│   │   │   ├── Client.cs
│   │   │   ├── Pharmacy.cs
│   │   │   └── Product.cs
│   │   ├── Interfaces/                  # Contracts/Abstractions
│   │   │   ├── Authentication/          # Auth interfaces
│   │   │   ├── Persistence/             # Repository interfaces
│   │   │   └── Services/                # Service interfaces
│   │   └── Common/
│   │       ├── Constants/               # Application constants
│   │       └── Errors/                  # Domain errors
│   │
│   ├── PharmaGO.Infrastructure/         # Infrastructure Layer
│   │   ├── Authentication/              # JWT implementation
│   │   ├── Persistence/                 # EF Core implementation
│   │   │   ├── Base/                    # Base repository
│   │   │   ├── PharmaGOContext.cs       # DbContext
│   │   │   └── *Repository.cs           # Repository implementations
│   │   └── Services/                    # External services
│   │
│   └── PharmaGO.Contract/               # API Contracts (DTOs)
│       ├── Authentication/              # Auth DTOs
│       ├── Pharmacy/                    # Pharmacy DTOs
│       └── Product/                     # Product DTOs
│
└── tests/
    └── PharmaGO.UnitTests/              # Unit Tests
        ├── Systems/                     # Test organization
        └── Helpers/                     # Test helpers
```

## ✅ Prerequisites

Before running this project, ensure you have the following installed:

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [PostgreSQL 15+](https://www.postgresql.org/download/)
- [Git](https://git-scm.com/downloads)
- IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/), [JetBrains Rider](https://www.jetbrains.com/rider/), or [VS Code](https://code.visualstudio.com/)

### Optional Tools

- [pgAdmin 4](https://www.pgadmin.org/) - PostgreSQL GUI
- [Postman](https://www.postman.com/) or [Insomnia](https://insomnia.rest/) - API testing
- [Docker](https://www.docker.com/) - For containerized PostgreSQL (optional)

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/pharmago.server.api.git
cd pharmago.server.api
```

### 2. Set Up PostgreSQL Database

#### Option A: Local PostgreSQL Installation

Create a new database:

```bash
# Connect to PostgreSQL
psql -U postgres

# Create database
CREATE DATABASE pharmago;

# Create user (if needed)
CREATE USER gildofj WITH PASSWORD 'your_password';

# Grant privileges
GRANT ALL PRIVILEGES ON DATABASE pharmago TO gildofj;
```

#### Option B: Using Docker

```bash
docker run --name pharmago-postgres \
  -e POSTGRES_DB=pharmago \
  -e POSTGRES_USER=gildofj \
  -e POSTGRES_PASSWORD=your_password \
  -p 5432:5432 \
  -d postgres:18
```

### 3. Restore NuGet Packages

```bash
dotnet restore
```

### 4. Build the Solution

```bash
dotnet build
```

## ⚙️ Configuration

### 1. Configure Application Settings

Update `src/PharmaGO.Api/appsettings.json` or create `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "JwtSettings": {
    "Secret": "your-secret-key-at-least-32-characters-long",
    "ExpiryMinutes": 60,
    "Issuer": "PharmaGO",
    "Audience": "PharmaGO"
  },
  "LicenseKeys": {
    "MediatR": "your-mediatr-license-key-if-needed"
  },
  "ConnectionStrings": {
    "PharmaGOContext": "Server=127.0.0.1;Port=5432;Database=pharmago;User Id=gildofj;Password=your_password;"
  }
}
```

### 2. Generate JWT Secret

For production, generate a secure secret key:

```bash
# Using PowerShell
$bytes = New-Object byte[] 32
[System.Security.Cryptography.RandomNumberGenerator]::Create().GetBytes($bytes)
[Convert]::ToBase64String($bytes)

# Using OpenSSL (Linux/Mac)
openssl rand -base64 32
```

### 3. Apply Database Migrations

```bash
# Navigate to the API project
cd src/PharmaGO.Api

# Create initial migration (if not exists)
dotnet ef migrations add InitialCreate --project ../PharmaGO.Infrastructure

# Apply migrations to database
dotnet ef database update
```

## 🏃 Running the Application

### Development Mode

```bash
# From solution root
dotnet run --project src/PharmaGO.Api/PharmaGO.Api.csproj

# Or navigate to API project
cd src/PharmaGO.Api
dotnet run
```

The API will start on:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`

### Watch Mode (Hot Reload)

```bash
cd src/PharmaGO.Api
dotnet watch run
```

### Production Build

```bash
dotnet publish -c Release -o ./publish
cd publish
dotnet PharmaGO.Api.dll
```

## 📚 API Documentation

### Interactive Documentation

Once the application is running in development mode, access the interactive API documentation:

**Scalar UI**: Navigate to `https://localhost:5001/scalar/v1`

### Available Endpoints

#### Authentication

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/Register` | Register new client |
| POST | `/api/auth/Login` | Login and get JWT token |

#### Pharmacies

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/pharmacies` | Create new pharmacy |
| GET | `/api/pharmacies/{id}` | Get pharmacy by ID |
| GET | `/api/pharmacies` | List all pharmacies |

#### Products

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/products` | Create new product |
| GET | `/api/products/{id}` | Get product by ID |
| GET | `/api/products` | List all products |

### Example Request

**Register Client:**

```bash
curl -X POST https://localhost:5001/api/auth/Register?pharmacyId=<pharmacy-guid> \
  -H "Content-Type: application/json" \
  -d '{
    "firstName": "John",
    "lastName": "Doe",
    "email": "john.doe@example.com",
    "password": "SecurePassword123!"
  }'
```

**Response:**

```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

## 🧪 Testing

### Run All Tests

```bash
# From solution root
dotnet test

# With detailed output
dotnet test --verbosity detailed

# With coverage
dotnet test /p:CollectCoverage=true
```

### Run Specific Test Project

```bash
dotnet test tests/PharmaGO.UnitTests/PharmaGO.UnitTests.csproj
```

### Test Structure

- **Unit Tests**: Testing individual components in isolation
- **Mocking**: Using Moq for dependency mocking
- **Assertions**: FluentAssertions for readable test assertions

## 🎯 Key Features

### 1. **Robust Error Handling**

- Custom error types using ErrorOr pattern
- Centralized error handling with ProblemDetails
- Consistent error responses across the API

### 2. **Automatic Validation**

- FluentValidation validators for all commands
- Validation pipeline behavior intercepts invalid requests
- Clear validation error messages

### 3. **Secure Authentication**

- JWT token-based authentication
- BCrypt password hashing with salt
- Configurable token expiration

### 4. **Database Management**

- Entity Framework Core with code-first approach
- Repository pattern for data access
- PostgreSQL with async operations

### 5. **API Versioning Ready**

- Structured for easy API versioning
- OpenAPI documentation generation
- Scalar UI for beautiful API docs

## ✨ Best Practices Implemented

### 1. **SOLID Principles**

- **Single Responsibility**: Each class has one reason to change
- **Open/Closed**: Open for extension, closed for modification
- **Liskov Substitution**: Proper abstraction usage
- **Interface Segregation**: Focused interfaces
- **Dependency Inversion**: Depend on abstractions, not concretions

### 2. **Clean Code**

- Meaningful names for classes, methods, and variables
- Small, focused methods
- Proper commenting where necessary
- Consistent code formatting

### 3. **Security**

- Password hashing with BCrypt
- JWT token-based authentication
- Sensitive data in configuration (not hardcoded)
- CORS configuration for controlled access

### 4. **Testing**

- Unit test coverage for business logic
- Mocking for external dependencies
- Arrange-Act-Assert pattern
- Descriptive test names

### 5. **Configuration Management**

- Environment-specific configurations
- User secrets for development
- Options pattern for strongly-typed settings

### 6. **Separation of Concerns**

- Clear layer boundaries
- DTOs for API contracts
- Entities for domain models
- Mappers for transformations

### 7. **Async/Await**

- Asynchronous operations throughout
- Proper cancellation token usage
- Non-blocking I/O operations

### 8. **Dependency Injection**

- Constructor injection
- Service lifetime management
- Testable code through DI

## 🐳 Docker Support (Future Enhancement)

Docker support is planned for future releases. The application will be containerized for easy deployment.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Gildo Junior**
- Email: 1gildojunior@gmail.com
- Portfolio: [https://gildofj.github.io/portfolio](https://gildofj.github.io/portfolio)

## 🙏 Acknowledgments

- Clean Architecture principles by Robert C. Martin
- MediatR library by Jimmy Bogard
- ErrorOr pattern for functional error handling
- The .NET community for excellent tools and libraries

---

**Note**: This project is under active development. Features and documentation may change.