
# PassIn-Blazor Solution

PassIn-Blazor is an ASP.NET Core solution designed for event management. This application allows users to log in, register new events, and list all events. The solution is divided into several projects, each with a specific responsibility, ensuring a clean and maintainable architecture.

## Solution Structure

The solution consists of the following projects:

1. **PassIn.Api**  
   - This project hosts the API for the application. It handles communication with the database (SQL Server) and provides endpoints for the Blazor WebAssembly application.
   - The API integrates the following projects:
     - **PassIn.Infrastructure**: Provides the `DbContext` and database models.
     - **PassIn.Application**: Contains the UseCases of the application.
     - **PassIn.Communication**: Defines request and response models to ensure type safety in API interactions.
   - It also includes Identity for user authentication and route protection.

2. **PassIn.Web**  
   - A Blazor WebAssembly application that acts as the front-end of the solution.
   - It communicates with `PassIn.Api` to perform operations such as listing events, registering new events, and deleting events.
   - It uses the Identity system from the API to authenticate users and secure routes.

3. **PassIn.Infrastructure**  
   - Contains the database configuration, `DbContext`, and models for interacting with the SQL Server database.
   - Includes a `docker-compose.yml` file to create a Docker container with a SQL Server database for development and testing.

4. **PassIn.Application**  
   - Hosts the UseCases of the application, encapsulating business logic and ensuring separation of concerns.

5. **PassIn.Communication**  
   - Provides type-safe request and response models for API communication.

6. **PassIn.Exceptions**  
   - Contains custom exceptions to handle application-specific errors.

## Development Setup

### Prerequisites
- .NET 6 SDK or later
- Docker (for running the SQL Server container)
- A compatible IDE (e.g., Visual Studio or VS Code)

### Running the Application
1. Clone the repository.
2. Navigate to the `PassIn.Infrastructure` project and start the Docker container for the SQL Server database using the `docker-compose.yml` file:
   ```bash
   docker-compose up
   ```
3. Run the `PassIn.Api` project to start the API.
4. Run the `PassIn.Web` project to start the Blazor WebAssembly application.
5. Access the application in your browser.

## Features
- User authentication with Identity.
- Event management: create, list, and delete events.
- Clean architecture with a modular project structure.
- Dockerized SQL Server database for easy setup and portability.


