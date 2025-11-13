# E-CommerceApp

# High Level Folder Architecure of Catalog.API

src/
 ├── Api/                     → Presentation layer (controllers, endpoints)
 │    ├── Controllers/
 │    │     └── CategoryController.cs
 │    ├── Program.cs
 │    └── appsettings.json
 │
 ├── Application/              → CQRS handlers, commands, queries, DTOs, validators
 │    ├── Categories/
 │    │     ├── Commands/
 │    │     │     ├── CreateCategory/
 │    │     │     │     ├── CreateCategoryCommand.cs
 │    │     │     │     ├── CreateCategoryHandler.cs
 │    │     │     │     └── CreateCategoryValidator.cs
 │    │     ├── Queries/
 │    │     │     └── GetCategories/
 │    │     │           ├── GetCategoriesQuery.cs
 │    │     │           └── GetCategoriesHandler.cs
 │    │     └── DTOs/
 │    │           └── CategoryDto.cs
 │    ├── Common/
 │    │     ├── Interfaces/
 │    │     │     └── IApplicationDbContext.cs
 │    │     └── Behaviors/
 │    │           └── ValidationBehavior.cs
 │
 ├── Domain/                   → Business entities and value objects
 │    ├── Entities/
 │    │     └── Category.cs
 │    ├── ValueObjects/
 │    └── Events/
 │
 ├── Infrastructure/           → Data persistence, EF Core, repositories, external services
 │    ├── Persistence/
 │    │     ├── ApplicationDbContext.cs
 │    │     ├── Configurations/
 │    │     │     └── CategoryConfiguration.cs
 │    │     └── DependencyInjection.cs
 │    └── Services/
 │
 └── SharedKernel/ (optional)  → Cross-cutting utilities (e.g., Result, Errors)

