# ECommerce API

**A layered, modular .NET Core 8 Web API designed for scalable online stores.**

---

## **Overview**
ECommerce API is a clean, extensible backend solution for e-commerce platforms, built using **Onion Architecture**. It ensures separation of concerns and testability, with support for caching, payment integration, and secure user authentication.

---

## **Technologies**
- **Framework:** ASP.NET Core 8
- **Languages:** C#
- **Databases:** SQL Server (Product/Order), Identity DB
- **Caching:** Redis
- **Payment:** Stripe Integration
- **Documentation:** Swagger (OpenAPI)

---

## **Features**
- **Onion Architecture:** Structured layering (Domain → Application → Infrastructure → Presentation)
- **EF Core (Code-First):** Strongly-typed models with migrations and relationships
- **Separate Identity Database:** JWT-secured authentication and role-based authorization
- **Redis Caching:** Speeds up data access and reduces database load
- **Stripe Integration:** Payment handling and webhook-based order confirmation
- **Specification Pattern:** Clean, reusable query filtering and sorting logic
- **Repository & Unit of Work:** Abstracted, testable data access
- **Clean Program.cs:** Service registration is modularized into separate layers
- **Static File Serving:** Product images served from `wwwroot/images/products`
- **Swagger UI:** For testing endpoints and exploring the API

---

## **Lessons Learned**

### What did I learn from building this project?
- **Clean Architecture:** Gained practical experience in applying Onion Architecture effectively.
- **Enterprise Patterns:** Applied design patterns like Specification, Repository, and Unit of Work.
- **Service Modularity:** Structured the code for easy scalability and separation of responsibilities.
- **API Security:** Implemented secure JWT tokens and multi-DB context handling for Identity.

### Challenges Faced and How I Solved Them
- **Many-to-Many Order Modeling:** Designed an `OrderItem` entity to manage quantity and pricing logic.
- **Stripe Webhooks:** Ensured atomic updates to order status with reliable webhook handling.
- **Layered Dependency Management:** Structured dependencies cleanly to support testability and future extensions.

---

## **Setup Instructions**

1. **Clone the repository:**
```bash
git clone https://github.com/AhmedUsama29/ECommerce.git
cd ECommerce
```

2. **Update configuration in `appsettings.json`:**
   - SQL Server connection strings for `ECommerceDb` and `IdentityDb`
   - Redis configuration
   - Stripe keys and webhook secret
   - JWT token settings

3. **Run migrations:**
```bash
cd Infrastructure
dotnet ef migrations add InitialECommerce --context ECommerceDbContext --output-dir Migrations/ECommerce
dotnet ef database update --context ECommerceDbContext

dotnet ef migrations add InitialIdentity --context IdentityDbContext --output-dir Migrations/Identity
dotnet ef database update --context IdentityDbContext
```

4. **Run the API:**
```bash
dotnet run --project Presentation/ECommerce.Api
```
Then navigate to `https://localhost:5001/swagger` to access Swagger UI.

---

## **Feedback**

Feel free to open issues or pull requests if you have suggestions or want to contribute!
