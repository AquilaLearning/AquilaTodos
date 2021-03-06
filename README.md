# Aquila Todo List App

## Prerequisites

- .NET Core SDK 3.1 - https://dotnet.microsoft.com/download/dotnet/3.1
- Node.js - https://nodejs.org/en/
- An appropriate IDE (e.g. VS Code)

## Developing Locally

### Run the api

- In a console navigate to the /AquilaTodos sub-directory
- Execute `dotnet watch run`

### Run the app

- In another console navigate to the /app sub-directory
- Install dependencies `npm install`
- Run the app `npm run serve`
- App should be accessible at http://localhost:8080/

## Unit Tests

### Run the api tests

- Open a console in the root of the solution
- Execute `dotnet test`

### Run the app tests

- Open a console in the /app sub-directory
- Execute: `npm test`
