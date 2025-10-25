
# 🧪 Fund Administration API – Technical Challenge

## 📘 Overview
This project is part of a technical challenge for a .NET Developer position. The goal is to implement a RESTful API for a Fund Administration platform that manages investment funds, investors, and their financial transactions (subscriptions and redemptions).

---

## 🧩 Business Domain
The API should support:
- **Funds**: Investment vehicles with basic metadata.
- **Investors**: Individuals investing in funds.
- **Transactions**: Financial operations linked to investors and funds.

---

## 🗃️ Entity Definitions

### 🏦 Fund
| Column       | Type      | Description                     |
|--------------|-----------|---------------------------------|
| `FundId`     | GUID      | Unique identifier               |
| `Name`       | string    | Fund name                       |
| `Currency`   | string    | Currency code (e.g., USD, EUR) |
| `LaunchDate` | DateTime  | Date the fund was launched      |

### 👤 Investor
| Column       | Type   | Description                         |
|--------------|--------|-------------------------------------|
| `InvestorId` | GUID   | Unique identifier                   |
| `FullName`   | string | Investor's full name                |
| `Email`      | string | Investor's email address            |
| `FundId`     | GUID   | Foreign key referencing a Fund      |

### 💸 Transaction
| Column          | Type     | Description                                      |
|-----------------|----------|--------------------------------------------------|
| `TransactionId` | GUID     | Unique identifier                                |
| `InvestorId`    | GUID     | Foreign key referencing an Investor              |
| `Type`          | enum     | Transaction type: `Subscription` or `Redemption` |
| `Amount`        | decimal  | Transaction amount (must be positive)            |
| `TransactionDate` | DateTime | Date of the transaction                        |

---

## 🛠️ Requirements

### Core Functionality
- CRUD operations for Funds and Investors
- POST endpoint to register Transactions
- GET endpoints to:
  - Retrieve all transactions for a specific investor
  - Get total subscribed and redeemed amounts per fund

### Architecture
- Use the Repository Pattern
- Apply DTOs
- Use Dependency Injection

### Validation & Error Handling
- Validate input data (e.g., positive transaction amounts)
- Global exception handling middleware
- Standardized error responses using ProblemDetails (RFC 7807)

### Documentation
- Swagger/OpenAPI with XML comments

### Security
- JWT Authentication (mocked or real)
- HTTPS enforcement

### Testing
- Unit tests for at least one service and one repository
- Use mocking for data access abstractions

---

## 🐳 Bonus Features (Optional)
- API versioning
- Health check endpoint
- Integration of **Serilog** for structured logging

---

## 📊 Reporting Endpoint (Bonus)
Create an endpoint that returns:
- Net investment per fund (subscriptions minus redemptions)
- Number of investors per fund

---

## 🧪 Evaluation Criteria
Your submission will be evaluated based on:
- Code clarity and organization
- Use of modern .NET development practices
- API design and REST principles
- Test coverage and maintainability
- Security and validation
- Documentation and usability

---

## 🚀 Getting Started
1. Clone the repository
2. Run the application using `dotnet run`
3. Access Swagger UI at `https://localhost:{port}/swagger`
4. Use Postman or Swagger to test endpoints

---

## 📦 Technologies
- ASP.NET Core (.NET 7+)
- Entity Framework Core
- Swagger / Swashbuckle
- xUnit / NUnit
- JWT Authentication
- Serilog

---

## 📬 Submission
Please submit your solution via GitHub or as a downloadable archive. If you have any questions, feel free to reach out.

Good luck!
