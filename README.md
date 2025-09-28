# Shop API

## Project Description
Shop API is a sample project built with **ASP.NET Core Web API**, designed following **Clean Architecture** principles.  
It demonstrates:
- Product Management (CRUD operations)
- User Registration & Login with JWT Authentication
- Order Management with transactional consistency

---

## Architecture
This project follows a layered architecture:

- **Domain Layer** – Entities: Product, User, Order, OrderItem
- **Application Layer** – DTOs, Interfaces, Validators, Security (PasswordHasher)
- **Infrastructure Layer** – EF Core DbContext, Repositories, Services, ADO.NET repository
- **API Layer** – Controllers, Swagger/OpenAPI, JWT Authentication, Global Error Handling Middleware

---

## Technologies Used
- **ASP.NET Core 8 Web API**
- **Entity Framework Core** with **SQL Server**
- **Swagger / OpenAPI**
- **JWT Authentication**
- **Dependency Injection**
- **ADO.NET** (for the bonus task)

---

## Setup Instructions
- Clone the repository.
- Update **appsettings.json** with your connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ShopDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

- Apply EF Core migrations:

```powershell
Add-Migration InitialCreate
Update-Database
```

- Run the project:

```bash
dotnet run --project Shop.Api
```

- Open Swagger UI in your browser:

```
https://localhost:5001/swagger/index.html
```

---

## Authentication Flow
- Register a new user: `POST /api/users/register`
- Login to get JWT token: `POST /api/users/login`
- Click **Authorize** in Swagger and enter:

```
Bearer <your_token_here>
```

- Call protected endpoints such as `/api/orders`.

---

## Example Requests

### Register User
```json
POST /api/users/register
{
  "email": "john@example.com",
  "password": "secret123"
}
```

### Login
```json
POST /api/users/login
{
  "email": "john@example.com",
  "password": "secret123"
}
```

### Create Product
```json
POST /api/products
{
  "name": "Laptop",
  "description": "Powerful machine",
  "price": 1200,
  "stockQuantity": 10
}
```

### Create Order (Authorized)
```json
POST /api/orders
{
  "items": [
    { "productId": 1, "quantity": 2 }
  ]
}
```

---

## Scalability
The API is stateless which allows horizontal scaling using a load balancer.  
EF Core DbContext is scoped per request, ensuring thread safety.  
Asynchronous controllers and repository methods allow the API to handle high concurrency efficiently.

---

## SOLID Principles
- **S** (Single Responsibility): Each service has a single clear responsibility.
- **O** (Open/Closed): The system can be extended by adding new services without modifying existing code.
- **L** (Liskov): Interfaces can be substituted with mock implementations for testing.
- **I** (Interface Segregation): Interfaces are small and focused on a single entity type.
- **D** (Dependency Inversion): Controllers depend on abstractions, not concrete implementations.

---

## OpenAPI / Swagger
The API is fully documented with Swagger.  
You can test endpoints directly via `/swagger`, including JWT-protected endpoints.  
Swagger includes detailed request/response examples and JWT authorization support.

---

## Bonus (ADO.NET)
`ProductSqlRepository` demonstrates raw ADO.NET usage for fetching products, fulfilling the bonus requirement.

---

## Project Status
✔ Product CRUD  
✔ User registration & login with JWT  
✔ Transactional order creation  
✔ Swagger documentation  
✔ Horizontal scalability explanation  
✔ SOLID principles applied  
✔ Bonus ADO.NET implemented
