# 🍦 Advanced Vending Machine System

A comprehensive ASP.NET Core Web API for managing an intelligent ice cream vending machine with state machine architecture, payment processing, and real-time tracking.

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Architecture](#project-architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Database Setup](#database-setup)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [State Machine Flow](#state-machine-flow)
- [Testing](#testing)
- [Contributing](#contributing)

## ✨ Features

- **State Machine Architecture**: Manages vending machine states (Idle, Item Selection, Payment, Dispensing)
- **Payment Processing**: Integrated with Stripe for secure payments
- **Product Management**: Full CRUD operations for ice cream products
- **Transaction Tracking**: Complete audit trail of all transactions
- **QR Code Scanning**: Initiates purchase flow via QR scan
- **Email Notifications**: Automated confirmation emails after purchase
- **Redis Caching**: Optimized product data retrieval
- **JWT Authentication**: Secure API access with role-based authorization
- **Image Upload**: Product image management
- **State History**: Complete log of all state transitions

## 🛠 Tech Stack

### Backend
- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core 9.0**
- **SQL Server**

### Libraries & Frameworks
- **Mapster** - Object mapping
- **Stripe.NET** - Payment processing
- **StackExchange.Redis** - Caching
- **ASP.NET Core Identity** - Authentication & Authorization
- **JWT Bearer** - Token-based authentication

### Design Patterns
- **State Machine Pattern**
- **Observer Pattern**
- **Repository Pattern**
- **Dependency Injection**

## 🏗 Project Architecture

```
Advanced-Vending-Machine/
├── VendingMachine.PL/          # Presentation Layer (API Controllers)
├── VendingMachine.BLL/         # Business Logic Layer
│   ├── Service/                # Business services
│   └── StateMachine/           # State machine implementation
└── VendingMachine.DAL/         # Data Access Layer
    ├── Data/                   # DbContext
    ├── Model/                  # Entity models
    ├── Repository/             # Data repositories
    ├── DTO/                    # Data Transfer Objects
    ├── Enums/                  # Enumerations
    └── Migrations/             # EF Core migrations
```

## 📦 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB or full version)
- [Redis](https://redis.io/download) (Optional for caching)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Stripe Account](https://stripe.com/) for payment processing

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/advanced-vending-machine.git
cd advanced-vending-machine
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Install Entity Framework Tools

```bash
dotnet tool install --global dotnet-ef
```

## ⚙️ Configuration

### 1. Create `appsettings.json`

Create `appsettings.json` in the `VendingMachine.PL` directory:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=VendingMachineDB;Trusted_Connection=True;TrustServerCertificate=True;",
    "Redis": "localhost:6379"
  },
  "Jwt": {
    "SecretKey": "YourSuperSecretKeyHereMustBeLongEnough123456789"
  },
  "Stripe": {
    "SecretKey": "sk_test_your_stripe_secret_key"
  },
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "EnableSsl": true,
    "Email": "your-email@gmail.com",
    "Password": "your-app-password"
  }
}
```

### 2. Configure Stripe

1. Sign up at [Stripe](https://stripe.com/)
2. Get your API keys from the Dashboard
3. Add your secret key to `appsettings.json`

### 3. Configure Email (Optional)

For Gmail:
1. Enable 2-factor authentication
2. Generate an app-specific password
3. Add credentials to `appsettings.json`

### 4. Configure Redis (Optional)

If not using Redis, you can comment out the Redis configuration in `Program.cs`.

## 🗄️ Database Setup

### 1. Update Connection String

Edit the connection string in `appsettings.json` to match your SQL Server instance:

```json
"DefaultConnection": "Server=YOUR_SERVER;Database=VendingMachineDB;Trusted_Connection=True;TrustServerCertificate=True;"
```

### 2. Apply Migrations

```bash
cd VendingMachine.PL
dotnet ef database update
```

### 3. Seeded Data

The application automatically seeds:

**Products:**
- Vanilla Ice Cream - $5.00
- Chocolate Ice Cream - $5.50
- Strawberry Ice Cream - $6.00
- Mint Ice Cream - $5.75
- Cookie Dough Ice Cream - $6.50

**Users:**
| Email | Password | Role |
|-------|----------|------|
| Lama@gmail.com | La@000 | Admin |
| nemeh@gmail.com | Ly@000 | Admin |
| layal@gmail.com | na@000 | Customer |

## 🏃 Running the Application

### Development

```bash
cd VendingMachine.PL
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:7259`
- HTTP: `http://localhost:5038`

### Production Build

```bash
dotnet publish -c Release -o ./publish
```

## 📚 API Documentation

### Base URL
```
https://localhost:7259/api/v1
```

### Authentication

#### Login
```http
POST /Accounts/Login
Content-Type: application/json

{
  "email": "Lama@gmail.com",
  "password": "La@000"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

#### Confirm Email
```http
GET /Accounts/ConfirmEmail?token={token}&userId={userId}
```

### Products

#### Get All Products
```http
GET /Products
```

#### Get Product by ID
```http
GET /Products/{id}
```

#### Create Product (Admin Only)
```http
POST /Products
Authorization: Bearer {token}
Content-Type: multipart/form-data

Name: Vanilla Ice Cream
Price: 5.0
Quantity: 50
MainImage: [file]
```

#### Update Product (Admin Only)
```http
PATCH /Products/{id}
Authorization: Bearer {token}
Content-Type: multipart/form-data

Name: Updated Name
Price: 6.0
Quantity: 40
MainImage: [file]
```

#### Toggle Product Status (Admin Only)
```http
PATCH /Products/{id}/toggle-status
Authorization: Bearer {token}
```

#### Delete Product (Admin Only)
```http
DELETE /Products/{id}
Authorization: Bearer {token}
```

### Transactions

#### Create Transaction
```http
POST /Transactions
Authorization: Bearer {token}
Content-Type: application/json

{
  "productId": 1
}
```

#### Get Transaction
```http
GET /Transactions/{id}
Authorization: Bearer {token}
```

#### Delete Transaction
```http
DELETE /Transactions/{id}
Authorization: Bearer {token}
```

### Checkout

#### Process Payment
```http
POST /CheckOut/payment
Authorization: Bearer {token}
Content-Type: application/json

{
  "transactionId": 1
}
```

**Response:**
```json
{
  "success": true,
  "message": "Payment session created successfully",
  "url": "https://checkout.stripe.com/...",
  "paymentId": "cs_test_..."
}
```

#### Payment Success (Webhook)
```http
GET /CheckOut/success/{session_id}/{transactionId}
```

#### Payment Cancel (Webhook)
```http
GET /CheckOut/cancel
```

### QR Scan

#### Scan QR Code
```http
POST /Scan/scan-qr
Content-Type: application/json

{
  "qrCode": "VEND-12345"
}
```

### Vending Machine State

#### Trigger Event
```http
POST /vending/trigger?evt=QR_Scanned
```

**Available Events:**
- `QR_Scanned`
- `Item_Selected`
- `Payment_Initiated`
- `Payment_Confirmed`
- `Payment_Failed`
- `Dispense_Complete`
- `Reset`
- `Error_Occurred`

#### Get Current State
```http
GET /vending/state
```

**Response:**
```json
{
  "state": "Idle"
}
```

#### Get State History
```http
GET /vending/history
```

**Response:**
```json
{
  "state": [
    {
      "id": 1,
      "stateBefore": "Idle",
      "stateAfter": "ItemSelection",
      "eventTriggered": "QR_Scanned",
      "timestamp": "2025-12-14T10:00:00"
    }
  ]
}
```

## 🔄 State Machine Flow

```
┌─────────────────────────────────────────────────────────┐
│                    State Transitions                     │
└─────────────────────────────────────────────────────────┘

Idle 
  └─[QR_Scanned]──> ItemSelection
                      └─[Item_Selected]──> PaymentPending
                                             └─[Payment_Initiated]──> PaymentProcessing
                                                                       ├─[Payment_Confirmed]──> ProductDispensing
                                                                       │                          └─[Dispense_Complete]──> Idle
                                                                       └─[Payment_Failed]──> ItemSelection

[Error_Occurred] from any state ──> Error ──> Idle
```

### States Description

| State | Description |
|-------|-------------|
| **Idle** | Machine waiting for customer interaction |
| **ItemSelection** | Customer browsing and selecting products |
| **PaymentPending** | Waiting for payment initiation |
| **PaymentProcessing** | Processing payment through Stripe |
| **ProductDispensing** | Dispensing the selected product |
| **Error** | Error state for handling failures |

## 🧪 Testing

### Using Postman/Thunder Client

1. Import the API endpoints
2. Login to get JWT token
3. Add token to Authorization header
4. Test endpoints

### Example Test Flow

```bash
# 1. Login
POST /api/v1/Accounts/Login
{
  "email": "Lama@gmail.com",
  "password": "La@000"
}

# 2. Get Products
GET /api/v1/Products

# 3. Create Transaction
POST /api/v1/Transactions
{
  "productId": 1
}

# 4. Process Payment
POST /api/v1/CheckOut/payment
{
  "transactionId": 1
}
```

## 🔐 Security

- JWT Bearer token authentication
- Role-based authorization (Admin/Customer)
- Password hashing with ASP.NET Identity
- Email confirmation required
- Account lockout after failed login attempts
- HTTPS enforcement
- SQL injection protection via EF Core

## 📝 Common Issues & Solutions

### Issue: Database connection fails
**Solution:** Verify SQL Server is running and connection string is correct

### Issue: Redis connection error
**Solution:** Comment out Redis configuration or install Redis locally

### Issue: Email not sending
**Solution:** Check SMTP settings and ensure app password is used for Gmail

### Issue: Stripe payment fails
**Solution:** Verify Stripe API key is correct and in test mode

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License.

## 👥 Authors

- **Lama Rafat** - Initial work
- **Nemeh Fayyad** - Contributor
- **Layal Rafat** - Contributor

## 🙏 Acknowledgments

- Stripe for payment processing
- Redis for caching solutions
- Microsoft for .NET and Entity Framework
- The open-source community



---

**Built with ❤️ using .NET 9.0**
