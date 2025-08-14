<div align="center">
  <h1>
    Customer Settlement & Palindrome API
  </h1>
  <p>
    A robust .NET RESTful API project that solves two real-world problems: <br/>
    <b>1. Valid Palindrome Check:</b> Efficiently determines whether a string is a palindrome, considering only alphanumeric characters and ignoring cases.<br/>
    <b>2. Customer Daily Settlement:</b> Calculates transaction fees, interacts with a banking API to settle customer funds, and stores all settlements securely using Entity Framework Core and SQL Server.<br/><br/>
    The project demonstrates clean architecture, code-first database design, layered business logic, robust validation, health checks, and full test coverage with NUnit.
  </p>
</div>

# Key Features

* **Palindrome Validation API:** Checks if an input string is a valid palindrome, returning a normalized result.
* **Customer Daily Settlement:** Calculates fees (11c per R100), deducts from the total, and posts funds to a (test) bank API.
* **Entity Framework Code-First:** Models, migrations, and DB context for SQL Server.
* **Clean Architecture:** Logical separation into Controllers, Models, Services, and Repositories.
* **Robust Validation & Error Handling:** Comprehensive validation and structured exception handling at every layer.
* **NUnit Test Coverage:** Unit tests for Controllers, Services, and Repositories.
* **Swagger/OpenAPI Documentation:** Interactive API docs out-of-the-box.
* **Health Checks:** For both API and SQL DB connectivity.
* **Configurable:** All endpoints, connection strings, and external API URLs are set in `appsettings.json`.

# Project Structure

* **Controllers:**  
  - `PalindromeController.cs`  
  - `CustomerSettlementController.cs`  
* **Models:**  
  - `/Entities`: Customer, SettlementTransaction  
  - `/Requests`: PalindromeRequest, SettleCustomerRequest  
  - `/Responses`: PalindromeResponse, SettleCustomerResponse  
* **Services:**  
  - `PalindromeService.cs`,  
  - `CustomerSettlementService.cs`  
* **Repositories:**  
  - `CustomerRepository.cs`,  
  - `SettlementRepository.cs`  
* **Data:**  
  - `AppDbContext.cs` (Entity Framework Code First)  
* **Tests (NUnit):**  
  - `/Controllers`  
  - `/Services`  
  - `/Repositories`  
* **Configuration:**  
  - `appsettings.json` 

# Getting Started

## Prerequisites

- .NET 6.0 SDK+
- SQL Server (or compatible local/remote instance)
- Visual Studio / VS Code / JetBrains Rider (or any IDE)
- [EF Core Tools](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Setup Steps

1. **Clone the Repository**
    ```bash
    git clone https://github.com/Zizwemkz/ConnectAssessment.git
    cd your-repo
    ```

2. **Configure Database**
    - Update your connection string in `appsettings.json` under `ConnectionStrings:DefaultConnection`.
    - Create the database using EF Core migrations:
      ```bash
      dotnet ef database update
      ```

3. **Configure External APIs**
    - Set the banking API endpoint in `appsettings.json`:
      ```json
      "BankApi": {
        "BaseUrl": "https://testbank/postfunds.co.za"
      }
      ```

4. **Build the Solution**
    ```bash
    dotnet build
    ```

5. **Run the Application**
    ```bash
    dotnet run
    ```

6. **Swagger Documentation**
    - Navigate to `http://localhost:5000/swagger` (or your configured port) for interactive API docs.

7. **Health Checks**
    - Endpoint: `/health`  
      Returns status of API and SQL DB.

## Running Tests

Navigate to the `Tests` directory and run:
```bash
dotnet test
```

## Design
## High Level Architecture Diagrme can be found here: 
![Design diagrame](./design/connectAss2.png)</a>

## Class Diagrame:
![Design diagrame](./design/connectass1.png)</a>
