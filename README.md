# About the Project

The backend component of the project for participation in the IT-Planet 2026 Olympiad in the "Applied Programming" category.

All the code in this project was written by author without using AI. AI only used to help create the documentation.

# Running the Application

To run the application, follow these steps:
1. Install Docker.
2. Clone the repository using the command: `git clone <repository_url>`
3. Navigate to the `infrastructure/build/powershell` directory and run `build-local.ps1` with powershell to start the application and all its dependencies.
4. The application will be available at: `http://localhost:3333/swagger`

# Repository Structure

- .github - Configuration for GitHub CI/CD workflows.
- docs - Project documentation.
- infrastructure - Additional tools and base configurations for deployment and monitoring.
- src - Source code.

# Stack

Technologies used during the development process:

## Code
- **.NET 10.0** - Primary framework.
- **MediatR** - Implementation of the CQRS pattern for command and query separation.
- **FluentValidation** - Model validation using a fluent interface.
- **NetTopologySuite** - Support for geospatial data and spatial geometry.
- **Serilog** - Structured logging.
- **Swagger** - OpenAPI documentation generation and interactive UI.
- **xUnit** - Testing framework.
- **AutoFixture** - Mock data generator.
- **Testcontainers** - Running ephemeral services in Docker for full-scale integration tests.
- **Respawn** - Quick database state cleanup between test runs.
- **Microsoft.AspNetCore.Mvc.Testing** - Tools for creating Web API integration tests.
- **coverlet.collector** - Data collection and analysis for code coverage.

## Data
- PostgreSQL (OLTP database)
- Redis (Cache)
- Kafka (Event sourcing)

## Integrations
- React frontend application - [GitHub](https://github.com/officer04/launchpad-front/tree/master)

# Contacts
You can contact me via:
- Telegram: [@egor4yt](https://t.me/egor4yt)
- Gmail: [egor4yt@gmail.com](mailto:egor4yt@gmail.com)