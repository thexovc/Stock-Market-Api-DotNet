# Stock Market API

## Overview

This is a **Stock Market API** built using **ASP.NET Core**. It provides functionality for managing stocks and user comments related to stocks. The API allows users to:

- Create, update, retrieve, and delete **stocks**
- Create, update, retrieve, and delete **comments** on stocks
- Implement **database persistence** using **Entity Framework Core**
- Use **DTOs (Data Transfer Objects)** for request/response models
- Follow **RESTful API** principles

## Technologies Used

- **C#**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server** (or any configured database)
- **Dependency Injection**
- **Swagger** (for API documentation)

## Project Structure

```
stockMarket/
│── Controllers/          # API Controllers for Stocks and Comments
│── Dtos/                 # Data Transfer Objects (DTOs)
│── Interfaces/           # Repository Interfaces
│── Mappers/              # Mapping between Entities and DTOs
│── Models/               # Entity Models (Stock, Comment)
│── Repositories/         # Implementation of Data Repositories
│── Data/                 # Database Context and Migrations
│── Program.cs            # Application entry point
│── appsettings.json      # Configuration file
└── README.md             # Project documentation
```

## API Endpoints

### Stock Endpoints

| Method | Endpoint          | Description        |
| ------ | ----------------- | ------------------ |
| GET    | `/api/stock`      | Get all stocks     |
| GET    | `/api/stock/{id}` | Get stock by ID    |
| POST   | `/api/stock`      | Create a new stock |
| PUT    | `/api/stock/{id}` | Update stock by ID |
| DELETE | `/api/stock/{id}` | Delete stock by ID |

### Comment Endpoints

| Method | Endpoint                 | Description                  |
| ------ | ------------------------ | ---------------------------- |
| GET    | `/api/comment`           | Get all comments             |
| GET    | `/api/comment/{id}`      | Get comment by ID            |
| POST   | `/api/comment/{stockId}` | Create a comment for a stock |
| PUT    | `/api/comment/{id}`      | Update a comment by ID       |
| DELETE | `/api/comment/{id}`      | Delete a comment by ID       |

## Setup and Installation

### Prerequisites

- **.NET SDK 6.0+**
- **SQL Server** (or any configured database)
- **Postman** (or any API testing tool)

### Installation Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/thexovc/Stock-Market-Api-DotNet
   cd stock-market-api
   ```

2. **Setup the database**  
   Update the `appsettings.json` file with your database connection string:

   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=StockMarketDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

3. **Run Migrations**

   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the Application**

   ```bash
   dotnet run
   ```

5. **Open API Documentation (Swagger UI)**  
   Navigate to: [http://localhost:5000/swagger](http://localhost:5000/swagger)

## Future Improvements

- Implement **Authentication & Authorization**
- Add **Pagination & Filtering** for stock listings
- Implement **Caching** for performance optimization
- Improve error handling with **global exception handling**

## License

This project is **open-source** and free to use.
