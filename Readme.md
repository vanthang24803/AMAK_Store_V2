# Running the Application

This is a basic guide on how to run the ASP .NET 8 application.

## Prerequisites

To run the application, you will need to have the following installed on your machine:

- NET Core >= 8: [Download and Install NET Core](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Git: [Download and Install Git](https://git-scm.com/downloads)

## Environment
Setup env before run dockerfile:

```env
POSTGRES_DB=
POSTGRES_USER=
POSTGRES_PASSWORD=
```

## Running Application

Build application

```bash
sh build.sh
```

Migration database using postgres 16

```bash
sh migrate.sh
```

Run application

```bash
sh run.sh
```

Build docker image

```bash
sh docker.sh "something"
```

## License

This project is licensed under the [MIT License](LICENSE).
