# 🏧 ATM Simulator

A console-based ATM and banking management system built with **C#** and **.NET**, structured using a clean **Layered Architecture** (`Domain`, `Application`, `Repository`, and `UI`). The application supports role-based access (`Client` and `Admin`), secure password hashing, account transactions, loan management, and persistent transaction logging.

---

## ✨ Features

### 👤 Client Features
* **Authentication & Security:** User registration and login with password hashing (`Hasher`).
* **Balance & Account Management:** Check current account balance in real time.
* **Transactions:** Deposit funds, withdraw money (with balance validation), and transfer money between accounts.
* **Loan Requests:** Apply for loans with automatic validation and track loan approval status (`Pending`, `Approved`, `Declined`).
* **Transaction History:** View personal transaction logs with timestamps.

### 🛡️ Admin Features
* **User Management:** View and manage registered users (`Admin` and `Client` roles).
* **Loan Administration:** Review client loan applications and approve or decline requests.
* **System Monitoring:** Access system-wide logs and transaction records.

---

## 🏗️ Architecture & Project Structure

The solution (`atm-simulator.sln`) is divided into 4 decoupled projects following Separation of Concerns and Repository Pattern principles:

    atm-simulator/
    ├── Domain/                     # Core business models & contracts
    │   ├── Entities/               # User, Client, Admin, Loan, Transaction
    │   ├── Enums/                  # Role (Admin, Client), LoanStatus (Pending, Approved, Declined)
    │   ├── Helpers/                # Hasher, Logger, TransactionLogger
    │   └── Interfaces/             # IUserRepository, ILoanRepository, ITransactionRepository
    │
    ├── Application/                # Business logic layer
    │   ├── Interfaces/             # Service contracts
    │   └── Services/               # AuthService, LoanService, TransactionService
    │
    ├── Repository/                 # Data access & persistence layer
    │   ├── Data/                   # Data storage files (JSON / file storage)
    │   └── Implementations/        # UserRepository, LoanRepository, TransactionRepository
    │
    └── UI/                         # Presentation layer (Console)
        ├── Menus/                  # ClientMenu, AdminMenu
        └── Program.cs              # Application entry point & dependency wiring

---

## 🛠️ Technologies & Concepts Used

* **Language:** C# / .NET
* **Architecture:** Multi-Layer Architecture (Domain, Application, Repository, UI)
* **Design Patterns:** Repository Pattern, Dependency Injection (Manual Constructor Injection), Separation of Concerns
* **Security & Auditing:** Password Hashing (`Hasher`) & File-based Logging (`Logger`, `TransactionLogger`)

---

## 🚀 Getting Started

### Prerequisites
* [.NET SDK](https://dotnet.microsoft.com/download) (6.0 or later)
* Visual Studio 2022 / VS Code / JetBrains Rider

### Installation & Running

1. **Clone the repository:**

       git clone https://github.com/abashidzeluka/atm-simulator
       cd atm-simulator

2. **Build the solution:**

       dotnet build

3. **Run the application:**

       dotnet run --project UI

---

## 📌 Future Improvements

* Migrate file-based data persistence to **Entity Framework Core** with **SQL Server / SQLite**.
* Add unit tests (`xUnit` / `NUnit`) for `AuthService`, `TransactionService`, and `LoanService`.
* Expose business logic via an **ASP.NET Core Web API**.
