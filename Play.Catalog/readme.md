# Command for the new api
- dotnet new webapi -n Play.Catalog.Services

### Play.Catalog.Services.csproj
- How to the project will be build

### Program.cs
- Main entrypoint for the application(the host)

### Startup.cs
- How the services will be used across application
- Service registring

### Controllers folder
- Every controller  for the application

### launchsettings.json
- Applicarion url / microservices address
- Verify "applicationUrl" property

### tasks.json
- How we are going to build the code from vscode
- Add property "group"
  - "group": { "kind": "build", "isDefault": true }

### launch.json
- How we are going to launch the application from vscode

# How to build the project:
- dotnet build

# How to run the project:
- dotnet run

# Make the project with a trustable certificated dev environment
- dotnet dev-certs https --trust

