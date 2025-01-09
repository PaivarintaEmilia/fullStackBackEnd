Project Overview
================

Introduction
------------

This project is a comprehensive **ASP.NET Core Web API** application designed to demonstrate advanced skills in software development. The application manages users, categories, expenses, and incomes, incorporating authentication, role-based access control, and data handling with a **SQLite database**. It also includes **Swagger documentation** for API testing and validation.

This project highlights my expertise in:

*   Backend development with **ASP.NET Core**.
    
*   Data modeling, querying, and manipulation with **Entity Framework Core** and **LINQ**.
    
*   REST API design and **JWT-based authentication**.
    
*   Dependency injection, service layer design, and clean architecture.
    

Key Features
------------

### Authentication and Authorization

*   **JWT Authentication**: Secure token-based authentication for users.
    
*   **Role-based Access Control**: Differentiates access for admin and user roles in endpoints.
    

### User Management

*   **User Registration**: Users can register with email and password.
    
*   **User Login**: Validates credentials and returns a JWT token.
    

### Expense Management

*   **CRUD Operations**: Create, read, update, and delete expenses.
    
*   **Categorization**: Expenses are categorized, supporting both user-defined and pre-defined categories.
    
*   **Date-Range Filtering**: Retrieve expenses within specific date ranges.
    

### Income Management

*   **CRUD Operations**: Manage income records.
    
*   **Date-Range Filtering**: Retrieve incomes within specific date ranges.
    
*   **Monthly Summaries**: Get the total sum of incomes for the current month.
    

### Data Validation and Mapping

*   **DTOs (Data Transfer Objects)**: Ensure data consistency and security.
    
*   **AutoMapper Integration**: Simplifies mapping between domain models and DTOs.
    

### Database Integration

*   **SQLite Database**: Efficient storage and retrieval of data.
    
*   **Entity Relationships**: Relationships between users, categories, expenses, and incomes are fully modeled.
    

Installation and Setup
----------------------

### Prerequisites

*   .NET 7 SDK
    
*   SQLite database engine
    
*   Visual Studio or any IDE supporting .NET development
    

### Steps

1.  Clone the repository:

```
git clone https://github.com/your-repository-urlcd your-repository-url
```
    
2.  Update the database connection string in 'appsettings.json':

```
{ "ConnectionStrings": { "DefaultConnection": "Data Source=your-database-file.db" }}
```
    
3.  Run migrations to set up the database:

```
dotnet ef migrations add InitialCreatedotnet 
```
```
dotnet ef database update
```
    
4.  Start the application:

```
dotnet watch
```
    
5.  Open Swagger at http://localhost:/swagger to test the endpoints.

```
http://localhost:/swagger
```
    

API Endpoints
-------------

### Users

*   **POST /api/Users/register**: Register a new user.
    
*   **POST /api/Users/login**: Login and retrieve a JWT token.
    

### Expenses

*   **GET /api/Expenses**: Retrieve expenses grouped by categories.
    
*   **GET /api/Expenses/total-amount**: Get the total sum of expenses for the current month.
    
*   **POST /api/Expenses**: Add a new expense.
    
*   **PATCH /api/Expenses/{id}**: Update an existing expense.
    
*   **DELETE /api/Expenses/{id}**: Delete an expense.
    

### Incomes

*   **GET /api/Incomes/get-incomes-by-date**: Retrieve incomes within a specific date range.
    
*   **GET /api/Incomes/total-amount**: Get the total sum of incomes for the current month.
    
*   **POST /api/Incomes**: Add a new income.
    
*   **PATCH /api/Incomes/{id}**: Update an existing income.
    
*   **DELETE /api/Incomes/{id}**: Delete an income.
    

### Categories

*   **GET /api/Categories**: Retrieve categories for the user.
    
*   **POST /api/Categories**: Add a new category.
    
*   **PUT /api/Categories/{id}**: Update a category.
    
*   **DELETE /api/Categories/{id}**: Delete a category.
    

Database Structure
------------------

### Tables

1.  **Users**
    
    *   Id: Primary Key
        
    *   Email, Role, PasswordSalt, HashedPassword
        
2.  **Categories**
    
    *   Id: Primary Key
        
    *   Name, UserDefined, UserId
        
3.  **Expenses**
    
    *   Id: Primary Key
        
    *   Amount, Description, CreatedAt, UserId, CategoryId
        
4.  **Incomes**
    
    *   Id: Primary Key
        
    *   Amount, Description, CreatedAt, UserId
        

Tools and Technologies
----------------------

*   **ASP.NET Core 7**
    
*   **Entity Framework Core**
    
*   **SQLite**
    
*   **Swagger (Swashbuckle)**
    
*   **AutoMapper**
    
*   **Dependency Injection**
    

Project Highlights
------------------

### Code Organization

*   Clean architecture principles with separate layers for controllers, services, and data models.
    
*   Extensive use of dependency injection for testable and maintainable code.
    

### Security

*   Password hashing with HMACSHA512.
    
*   JWT-based authentication with role-based access control.
    

### Testing

*   Swagger documentation simplifies testing and demonstrates the API’s capabilities.
    

Future Enhancements
-------------------

*   Add unit and integration tests.
    
*   Implement advanced filtering and sorting for expenses and incomes.
    
*   Enhance user management with password reset functionality.
    
*   Add support for multiple database providers (e.g., PostgreSQL).
    




