# MrWorldWide Web Server
MrWorldWide is a web server for a digital nomad to keep their friends informed about their current location, as well as a blog and photo journal. This project is built using .NET 7 and deployed in a Docker container.

## Getting Started
### Prerequisites
To set up a local development environment, ensure you have the following dependencies installed:

- .NET 7 SDK
- Docker
- PostgreSQL

### Installation
1. Clone the repository:
```bash
git clone https://github.com/your-username/mrworldwide.git
```
2. Navigate to the server directory
3. Restore .NET packages
4. Run the application
```bash
cd .\server\src\
dotnet restore
dotnet run .\MrWorldWide.Api\MrWorldWide.Api.csproj
```

## Technical Details
**Data Storage**: The data storage for the web server is backed by PostgreSQL, a powerful and open-source relational database management system. To simplify and streamline database operations, the application uses Marten, a document database and event store built on top of PostgreSQL. Marten provides an easy-to-use abstraction for handling data storage and retrieval, allowing the application to leverage PostgreSQL's capabilities efficiently.

**Project Structure**: The solution contains two projects - an API project and a tests project. The API project contains the web server implementation, and the tests project holds the unit and integration tests. Given the limited scope of the project, this structure is suitable for maintaining a clean separation of concerns while minimizing complexity. However, developers collaborating on this project are expected to adhere to best practices and limit coupling between components that would typically be in different assemblies.

**Architecture**: The application employs a vertical slice architecture style, which organizes the codebase into functional units or "slices" instead of the traditional layered architecture. Each vertical slice encapsulates a specific feature or functionality, including its business logic, data access, and presentation concerns. This approach promotes high cohesion, low coupling, and improved maintainability by reducing the impact of changes to individual slices.

**Docker Deployment**: The web server is designed to be deployed in a Docker container, which provides an isolated and consistent runtime environment. Docker simplifies deployment and scaling by allowing the application to run anywhere Docker is supported, making it easy to manage and deploy across various environments and infrastructure.

**Error Handling**: The application uses the ProblemDetails standard for transmitting error information in a consistent and machine-readable format. HTTP status codes are determined by a configured convention, where they are mapped to standard and custom exception types. This approach ensures that error handling is uniform and easy to understand, simplifying the process of debugging and resolving issues.