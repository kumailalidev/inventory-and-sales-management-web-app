# Inventory and Sales Management Web Application

## Overview
This is a feature-rich **Inventory and Sales Management Web Application** built with **ASP.NET Core MVC** using **N-Tier Clean Architecture**. It provides role-based access control using **ASP.NET Core Identity**, allowing administrators to manage inventory and transactions, sellers to process sales, and managers to generate reports.

## Features
- **Admin Role**: Manage products, categories, sales, and transaction reports.
- **Seller Role**: Perform sales operations only.
- **Manager Role**: Generate sales transaction reports.
- **Role-Based Authorization**: Secure access using **ASP.NET Core Identity**.
- **N-Tier Clean Architecture**: Well-structured separation of concerns.
- **SQL Server Database**: Efficient data storage and retrieval.

## Installation Instructions

### **Prerequisites**
Ensure you have the following installed:
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio Code](https://code.visualstudio.com/)
- [EF Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

### **Setup Steps**
1. **Clone the Repository**:
   ```sh
   git clone https://github.com/kumailalidev/inventory-and-sales-management-web-app.git
   cd inventory-and-sales-management-web-app
   ```

2. **Update the Database**
- Run the following commands to apply database migrations for ASP.NET Core Identity
    ```sh
    dotnet ef database update --context AccountContext --project src/WebApp
    ```
- Run the following commands to apply database migrations for Products, Categories and Transactions
    ```sh
    dotnet ef database update --context MarketContext --project src/Plugins/Plugins.DataStore.SQL --startup-project src/WebApp
    ```
3. **Run the Project**
    ```sh
    dotnet run --project src/WebApp
    ```