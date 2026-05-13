# Carting Service – RESTful API & Layered Architecture
 
This project implements the **Carting Service** as part of the .NET Mentoring Program. It demonstrates a clean, maintainable design using a **Layered Architecture** and adheres to **RESTful** principles, including multi-versioning support and self-documented contracts.
 
## Architecture Overview
 
The solution is organized into four main logical layers, ensuring a clear separation of concerns:
 
### Domain
- Defines core business entities: `Cart` and `CartItem`.
- Independent of frameworks, representing the pure business model.
 
### DAL (Data Access Layer)
- Manages data persistence using **LiteDB** (NoSQL).
- Implements the **Repository Pattern** to abstract storage logic.
 
### BLL (Business Logic Layer)
- Encapsulates business rules and workflows.
- Coordinates between the API and Data layers, ensuring data consistency and validation.
 
### Web API
- Provides the system's entry point via RESTful endpoints.
- Features **API Versioning** (v1 and v2) to support evolving client requirements.
- Integrated with **Swagger** and **XML Documentation** for a self-descriptive API.
 
## REST Implementation & Versioning
 
The API supports multiple versions to ensure backward compatibility and meet specific functional requirements:
 
*   **Version 1.0:** The `Get` endpoint returns a full **Cart Model** (Cart Key + List of Items).
*   **Version 2.0:** The `Get` endpoint is optimized to return only the **List of Items**.
*   **Self-Documented API:** All endpoints include detailed descriptions generated from **XML Documentation** tags in the source code.
 
## Non-Functional Requirements
 
### Testability
- High testability achieved through **Dependency Injection**.
- The BLL is thoroughly covered by **Unit Tests** using **xUnit** and **FluentAssertions**.
- **Moq** is used to isolate business logic from data access concerns by mocking the `ICartRepository`.
 
### Extensibility & Documentation
- **Versioning Strategy:** The use of URL-based versioning (`api/v{version}/[controller]`) allows seamless extensibility.
- **OpenAPI Standards:** Swagger is configured to provide separate documentation sets for each API version.
 
## Getting Started
 
Follow these steps to run the project locally:
 
1. Open the solution in Visual Studio 2022.
2. Set `CartingService.WebApi` as the Startup Project.
3. **Build the solution** to generate the required XML documentation file.
4. Press **F5** to run the application.
5. Use the version dropdown in the **Swagger UI** to switch between **v1** and **v2** definitions.