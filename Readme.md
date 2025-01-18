## What is AMAK?

AMAK API is an API for AMAK application written in .NET Core (version >= 8) and following the Clean Architecture pattern and Domain Driven Design.

## Technologies

- [x] [.NET Core (version >= 8)](https://dotnet.microsoft.com/)
- [x] [Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture)
- [x] [Domain Driven Design](https://domainlanguage.com/)
- [x] [CQRS/Event Sourcing](https://martinfowler.com/bliki/CQRS.html)
- [x] [RabbitMQ](https://www.rabbitmq.com/)
- [x] [Redis](https://redis.io/)
- [x] [PostgreSQL](https://www.postgresql.org/)
- [x] [Docker](https://www.docker.com/)
- [x] [Cloudinary](https://cloudinary.com/)
- [x] [CI/CD Github Actions](https://docs.github.com/en/actions)
- [x] [Unit Test With Moq](https://github.com/moq/moq4), [XUnit](https://xunit.net/)
- [x] [Nginx](https://nginx.org/en/)

## Roadmap

- [x] _~~Apply ElasticSearch for Search Engine~~_
- [ ] Apply Event Sourcing
- [ ] Apply Kafka for Event Bus
- [ ] Migrate for Microservices Application
- [ ] Cover all features by integration test

### Prerequisites

To run the application, you will need to have the following installed on your machine:

- NET Core >= 8: [Download and Install NET Core](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Git: [Download and Install Git](https://git-scm.com/downloads)

### Running Application

To run the application, follow these steps:

1. Clone the repository:

   ```bash
   git clone https://github.com/maynguyen24/AMAK.git
   ```

2. Navigate to the project directory:

   ```bash
   cd AMAK
   ```

3. Install the dependencies:

   ```bash
   dotnet restore
   ```

4. Build the project:

   ```bash
   sh build.sh
   ```

5. Run the project:

   ```bash
   sh run.sh
   ```

   PowerShell Terminal

   ```ps1
    run.ps1
   ```

## License

This project is licensed under the [MIT License](LICENSE).
