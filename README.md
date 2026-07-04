# 🔐 AuthAdminCrud.MVC

A layered **ASP.NET Core MVC (.NET 9)** application demonstrating cookie-based authentication with ASP.NET Core Identity, role-based authorization, and an isolated **Admin Panel** area for product CRUD management.

<p align="left">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet&logoColor=white" alt=".NET 9" />
  <img src="https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=flat&logo=dotnet&logoColor=white" alt="ASP.NET Core MVC" />
  <img src="https://img.shields.io/badge/EF_Core-9-512BD4?style=flat" alt="EF Core 9" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white" alt="SQL Server" />
</p>

---

## 📖 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Architecture](#-architecture)
- [Solution Structure](#-solution-structure)
- [Domain Model](#-domain-model)
- [Key Design Points](#-key-design-points)
- [Getting Started](#-getting-started)
- [License](#-license)
- [Author](#-author)

---

## 🧭 Overview

`AuthAdminCrud.MVC` is a conventional N-layer MVC application built to showcase a production-style authentication and authorization flow. It combines ASP.NET Core Identity (cookie auth) with an **Areas**-based split that physically and logically isolates the admin experience from the public site, plus domain-driven entities that enforce their own invariants.

---

## ✨ Features

- 🔑 **Cookie-based authentication** with ASP.NET Core Identity (Guid keys)
- 👥 **Role-based authorization** — `Admin` and `User` roles seeded on startup
- 🛠️ **Isolated Admin Panel** via Areas, locked behind `[Authorize(Roles = "Admin")]`
- 📦 **Product CRUD** with image upload (GUID-based unique naming)
- 🧩 **Fluent API configuration** centralized per entity
- 🧠 **Domain-driven entities** with encapsulated validation (private setters + guarded methods)
- 👤 **User profile** management (full name, avatar image)

---

## 🧱 Tech Stack

| Layer        | Technology                                                        |
| ------------ | ----------------------------------------------------------------- |
| Framework    | ASP.NET Core MVC (.NET 9.0)                                        |
| Auth         | ASP.NET Core Identity (`IdentityUser` / `IdentityRole` with Guid) |
| Data Access  | Entity Framework Core 9 (SQL Server / LocalDB)                     |
| Tooling      | EF Core Migrations, Code Generation Design tools                  |
| Frontend     | Razor Views, custom CSS, Bootstrap-based Admin theme, FontAwesome |

---

## 🏛️ Architecture

Conventional N-layer MVC structure with an **Areas** split isolating the admin experience from the public site.

**Architectural principles**

- Separation of Concerns (SoC)
- Role-based Authorization
- Area-based Modularization
- Fluent API Configuration
- Domain-driven entities with encapsulated validation

---

## 🧩 Solution Structure

```
AuthAdminCrud.MVC
│
├── Areas
│   └── AdminPanel
│       ├── Controllers        # DashBoardController, ProductController [Authorize(Roles="Admin")]
│       ├── Views              # Dashboard + Product CRUD views, _AdminLayout
│       └── Helper             # FileHelper (image upload handling)
│
├── Controllers
│   ├── AccountController.cs   # Login, Register, Logout, Profile, AccessDenied
│   └── HomeController.cs
│
├── Data
│   ├── AuthDbContext.cs       # IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
│   ├── Configurations         # Fluent API entity configs (IEntityTypeConfiguration<T>)
│   └── Seed                   # RoleSeed + SeederProgram (Admin/User roles + default admin)
│
├── Migrations
│
├── Models
│   ├── common
│   │   └── BaseEntity.cs      # Shared Guid Id base type
│   ├── AppUser.cs             # Extends IdentityUser<Guid> — FullName, ImageUrl, Basket
│   ├── Product.cs             # Rich domain model with validated setters
│   └── BasketItem.cs          # User-Product join entity with quantity
│
├── ViewModels                 # Login / Register / Profile / Product DTOs
│
├── Views
│   ├── Account                # Login, Register, Profile, AccessDenied
│   ├── Home
│   └── Shared                 # _Layout, validation partials
│
├── wwwroot
│   ├── admin                  # Admin theme static assets
│   ├── css
│   └── uploads                # User and product images
│
├── Program.cs                 # DI, Identity config, middleware pipeline, area routing
└── appsettings.json
```

---

## 🧠 Domain Model

```
AppUser (IdentityUser<Guid>)
├── FullName, ImageUrl
└── BasketItems (1:N)

Product (BaseEntity)
├── Name, Price, ImageUrl, ButtonText
└── BasketItems (1:N)

BasketItem (BaseEntity)
├── UserId → AppUser
├── ProductId → Product
└── Count
```

---

## 🎯 Key Design Points

**Domain-driven entities** — `Product` and `BasketItem` use private setters with validation methods (`SetName`, `SetPrice`, etc.), enforcing invariants at the model level rather than in controllers.

**Role-based authorization** — `Admin` and `User` roles are seeded automatically on startup via `SeederProgram.SeedData`. AdminPanel controllers are locked behind `[Authorize(Roles = "Admin")]`.

**Area-based routing** — admin routes are registered *before* the default route, keeping the admin module physically and logically separate from the public site.

**Fluent API configuration** — entity constraints are centralized in `Data/Configurations/` and applied via `ApplyConfigurationsFromAssembly`, keeping `AuthDbContext` clean.

**File upload handling** — `FileHelper` centralizes saving uploaded images to `wwwroot/uploads` with GUID-based unique naming.

**Design patterns** — Layered MVC · Repository-style DbContext abstraction · DTO / ViewModel pattern · Area-based modularization · Built-in Dependency Injection.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server or LocalDB
- EF Core tools: `dotnet tool install --global dotnet-ef`

### Installation

```bash
# 1. Clone the repository
git clone https://github.com/vusal016/AuthAdminCrud.MVC.git
cd AuthAdminCrud.MVC

# 2. Configure the connection string in appsettings.json
#    "DefaultConnection": "Server=...;Database=AuthAdminCrud;..."

# 3. Apply migrations
dotnet ef database update

# 4. Run the app
dotnet run
```

On first run, the seeder creates the `Admin` and `User` roles along with a default admin account. Check `Data/Seed/SeederProgram.cs` for the seeded admin credentials, and change them before any public deployment.
---

## 👤 Author

**Vusal Memmedov**

[![Gmail](https://img.shields.io/badge/Gmail-D14836?style=flat&logo=gmail&logoColor=white)](mailto:mvusal316@gmail.com)
[![LinkedIn](https://img.shields.io/badge/LinkedIn-0A66C2?style=flat&logo=linkedin&logoColor=white)](https://linkedin.com/in/vusalmemmedov)
[![GitHub](https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white)](https://github.com/vusal016)

---

<p align="center">⭐ If you find this project useful, consider giving it a star!</p>
