# HNG Stage2 Task
## Description
A simple REST API capable of CRUD operations on a "person" resource, interfacing with Sql Server database

## Table of Contents
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#Usage)

## Available Endpoints

| Endpoint                | Description                                | HTTP Methods |
|-------------------------|--------------------------------------------|--------------|
| `/api/`                 | Creates a new user.                        | POST         |
| `/api/{user_id}`        | Update a user.                             | PUT          |
| `/api/{user_id}`        | Fetches a user.                            | GET          |
| `/api/{user_id}`        | Deletes a user.                            | DELETE       |

## API Documentation

For detailed API documentation, including endpoint descriptions, request/response examples, and more, please refer to the [API Documentation](./Documentation.md) file.

## Prerequisites
Before you begin, ensure you have met the following requirements:

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet) (version 4.8 or higher)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (optional, for database integration)

## Installation
1. Clone the repository:

   ```bash
   git clone https://github.com/MaureenMOguche/HNGStage2.git
   cd your-api-project
   ```
2. Restore Dependencies
   ```
   dotnet restore
   ```
3. Build the project
   ```
   dotnet build
   ```
4. Run the project
   ```
   dotnet run
   ```
   Your API should now be running locally at http://localhost:5000

