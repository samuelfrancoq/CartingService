# Carting Service – Layered Architecture

This project implements the Carting Service as part of the .NET Mentoring Program. Its primary objective is to demonstrate a clean and maintainable design by applying the Layered Architecture pattern, ensuring a clear separation of concerns across the system.

## Architecture Overview

The solution is organized into four main logical layers:

### Domain
- Defines the core business entities such as `Cart` and `CartItem`.
- Completely independent of any frameworks or external dependencies.
- Represents the heart of the application.

### DAL (Data Access Layer)
- Responsible for data persistence.
- Uses LiteDB (NoSQL database) for storage.
- Implements the Repository Pattern to abstract data access logic.

### BLL (Business Logic Layer)
- Contains the core business rules and workflows.
- Coordinates communication between the API layer and the data layer.
- Ensures validation and business consistency.

### Web API
- Serves as the entry point of the application.
- Exposes RESTful endpoints.
- Integrated with Swagger for API documentation and testing.

## Non-Functional Requirements

### Testability
- Achieved through Dependency Injection and interface-based design.
- Business logic is covered with unit tests.
- Uses Moq to mock dependencies and isolate components during testing.

### Extensibility
- Designed to support future enhancements with minimal changes.
- Example: replacing LiteDB with another database provider only requires a new implementation of `ICartRepository`, without affecting the business logic.

## Getting Started

Follow these steps to run the project locally:

1. Open the solution in Visual Studio 2022.
2. Set `CartingService.WebApi` as the Startup Project.
3. Press `F5` to run the application.
4. The Swagger UI will open automatically in your browser.