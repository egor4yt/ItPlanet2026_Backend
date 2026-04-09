# About the Project

**Launchpad** is a modern platform that helps job seekers find their dream jobs and helps companies find the right talent.

All the code in this project was written by author without using AI. AI only used to help create the documentation.

# Running the Application

To run the application, follow these steps:
1. Install Docker.
2. Clone the repository using the command: `git clone <repository_url>`
3. Navigate to the `infrastructure/build/powershell` directory and run `build-local.ps1` with powershell to start the application and all its dependencies.

# Docker Containers

After you ran docker compose all your applications will be available on their ports:

| Container                                                   | Port               | Profile        |
|-------------------------------------------------------------|--------------------|----------------|
| launchpad.candidates.api                                    | 3333               | all, load-test |
| launchpad.database                                          | 1300               | all, load-test |
| launchpad.database.metrics                                  | 9180               | all, load-test |
| launchpad.keycloak<br/>_Identity Provider_<br/>_Management_ | <br/>1200<br/>1250 | all            |
| launchpad.prometheus                                        | 9080               | all, load-test |
| launchpad.grafana                                           | 3240               | all, load-test |
| launchpad.k6                                                | -                  | all, load-test |
| launchpad.elasticsearch                                     | 9228               | all, load-test |
| launchpad.logstash                                          | 5049               | all, load-test |
| launchpad.kibana                                            | 5609               | all, load-test |

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
- Grafana + Prometheus (Monitoring)
- Keycloak (OIDC provider)

## Integrations
- React frontend application - [GitHub](https://github.com/officer04/launchpad-front/tree/master)

# Contacts
You can contact me via:
- Telegram: [@egor4yt](https://t.me/egor4yt)
- Gmail: [egor4yt@gmail.com](mailto:egor4yt@gmail.com)