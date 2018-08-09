# Using Spring Boot Config Server with ASP.NET Core 2.1

Run *Vault*:
```bash
docker run -d -p 8200:8200 --name vault -e 'VAULT_DEV_ROOT_TOKEN_ID=myroot' -e 'VAULT_DEV_LISTEN_ADDRESS=0.0.0.0:8200' vault
```
Enter in *Vault* container and then;
```bash
docker exec -i -t vault sh
export VAULT_ADDR='http://localhost:8200'
vault auth myroot
```

Run *Spring Boot Config Server*:
```bash
cd Server
mvn spring-boot:run
```

Run *ASP.NET Core 2.1 MVC Project*:
```bash
cd Client
dotnet restore
dotnet build
dotnet run
```