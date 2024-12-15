# FactApp

## Overview

**FactApp** is a simple web application built with .NET 8.0 that provides an API for managing "facts." The application allows users to perform CRUD (Create, Read, Update, Delete) operations on facts, which are stored in a file. The API exposes endpoints for interacting with facts, and it uses Swagger for API documentation.

## Features

- **Create, Read, Update, Delete** facts through a Web API.
- **Swagger Integration** for easy API documentation and testing.
- **Dependency Injection** for easier testing and maintainability.
- **AutoMapper** for object-to-object mapping between layers.
- **DDD (Domain-Driven Design)** for clean architecture and maintainability.

## Technologies Used

- **.NET**: Framework for building RESTful APIs.
- **Swashbuckle.AspNetCore**: Adds Swagger support to the Web API for easy documentation and testing.
- **Dependency Injection (DI)**: Manages the application’s dependencies for better maintainability and testability.
- **AutoMapper**: Maps objects of different types, helping to separate the concerns of object transformation.
- **Domain-Driven Design (DDD)**: A design approach that focuses on the business domain, organizing code around real-world concepts.

## Project Structure

The solution contains the following main projects:

- **FactApp.WebAPI**: The Web API layer, exposing the application to external users.
- **FactApp.Application**: The application layer containing business logic and service implementations.
- **FactApp.Domain**: The domain layer containing core business models, entities, and domain services.
- **FactApp.Infrastructure**: The infrastructure layer handling external dependencies such as repositories and file storage.
- **Controllers**: Handles incoming HTTP requests and communicates with the application services.
- **Services**: Contains business logic and interacts with repositories to fetch or store data.
- **Repositories**: Responsible for interacting with data sources (e.g., text files) to retrieve, save, or delete facts.
- **Domain**: Contains the core business logic, including the definitions of facts and their validation rules.
- **Application**: Coordinates the flow between controllers, services, and repositories.

## API Endpoints

### `GET /api/facts`

Retrieves a list of facts from the specified file.

#### Parameters:

- `fileName` (optional): The name of the file to read facts from (default is `facts.txt`).
- `top` (optional): The number of facts to retrieve. If `0` or `null`, retrieves all available facts.

#### Response:

- **200 OK**: A list of facts from the file.
- **400 BadRequest**: If there is an invalid argument passed.
- **404 NotFound**: If no facts are found or the file does not exist.
- **409 Conflict**: If there is a conflict while fetching the facts.
- **500 Internal Server Error**: If an unexpected error occurs.

### `POST /api/facts`

Saves a new fact to the specified file.

#### Parameters:

- `fileName` (optional): The name of the file to save the fact (default is `facts.txt`).
- `count` (optional): The number of facts to save (default is `1`).

#### Response:

- **201 Created**: A fact or multiple facts have been successfully saved to the file.
- **400 BadRequest**: If an invalid argument is passed.
- **404 NotFound**: If the file cannot be found.
- **409 Conflict**: If a conflict arises while saving the fact.
- **500 Internal Server Error**: If an unexpected error occurs.

### `DELETE /api/facts`

Deletes a specified number of facts from the file.

#### Parameters:

- `count` (optional): The number of facts to delete. If null, deletes all facts.
- `fileName` (optional): The name of the file to delete facts from (default is `facts.txt`).

#### Response:

- **200 OK**: The facts were successfully deleted. The number of deleted facts is returned.
- **400 BadRequest**: If there is an invalid argument or issue with the file name or count.
- **404 NotFound**: If the file does not exist.
- **409 Conflict**: If there is a conflict during deletion.
- **500 Internal Server Error**: If an unexpected error occurs during the deletion process.
