# Cargo Coordination Platform back end implementation

The project was generated using the [Clean.Architecture.Solution.Template](https://github.com/jasontaylordev/CleanArchitecture) version 9.0.12.

## Getting started

Download or clone the project and run `dotnet build -tl` to build the solution.

## Run

To run the web application:

```bash
cd .\src\Web\
dotnet watch run
```

Navigate to https://localhost:5001. The application will automatically reload if you change any of the source files.

## How to authenticate?

Copy the following credentials as they will be seeded to the databases when you run the application.

```
administrator@localhost
Administrator1!
```

leave the 2 factor fields empty and send the request and get an access token, grab it and authorize yourself in swagger UI or any other tool you are using.

## Consuming APIs

Consuming the APIs is just simple, first we need to create a load by sending a request to Loads `POST` endpoint and then the reponse will contain an ID.

Grab the Id and now you can create a `Bid`. more then 1 bid cannot be create for a load as its prevented in validation.

And when finally we can call `Loads/{id}` of `PATCH` request to accept a load and create a `Trip` for it.