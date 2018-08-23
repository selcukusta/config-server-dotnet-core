# Using Spring Boot Config Server with ASP.NET Core 2.1

### Requirements:
> JDK 1.8

> Apache Maven 3.5.4

> Docker

> Dotnet 2.1 

---
Run *Vault*:
```bash
docker run -d -p 8200:8200 --name vault -e 'VAULT_DEV_ROOT_TOKEN_ID=myroot' -e 'VAULT_DEV_LISTEN_ADDRESS=0.0.0.0:8200' vault
```
Enter in *Vault* container and then;
```bash
docker exec -i -t vault sh
export VAULT_ADDR='http://localhost:8200'
```
To write sample config values to the Vault instance, run *ASP.NET Core 2.1 MVC ConfigWriter project*:
```bash
cd ConfigWriter
dotnet restore
dotnet build
dotnet run
```

Run *Spring Boot Config Server*:
```bash
cd Server
mvn spring-boot:run
```

Run *ASP.NET Core 2.1 MVC Client Project*:
```bash
cd Client
dotnet restore
dotnet build
dotnet run
```