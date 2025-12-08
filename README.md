# Prueba Técnica Backend Eduardo Cabrera
Este proyecto busca una implementacion de un api siguiendo los lineamientos solicitados en la prueba tecnica, el cual incluye:
- Arquitectura **Clean Architecture**
- Patrón **CQRS**
- **Inyección de Dependencias**
- Acceso a datos **mediante Stored Procedures**
- División clara de responsabilidades
- Ejemplos de endpoints solicitados

## 🧱 **Arquitectura y Patrones Implementados**

### 🏛 Clean Architecture
La solución se divide en las siguientes capas:

```
src/
 ├── WebAPI              (Capa de presentación)
 ├── Application         (Capa de casos de uso / CQRS)
 ├── Domain              (Entidades y contratos)
 ├── Infrastructure      (Implementación de repositorios + acceso a datos via SP)
```

## :hammer: Justificacion de la capa WebApi
- Se crea con con el fin de separar los DTO que exponen/reciben informacion del/al api, de los DTO de la capa de aplicacion, de esta forma se logra un mejor desacople enntre capas, de esta forma los DTO de aplicacion no los exponemos al api, 
por ejemplo en mi caso los SP que realizan tranformaciones retornan informacion sobre la transaccion, los DTO de aplicacion sirven para capturar esa informacion y realizar validaciones en en handler y en el api se retorna el DTO porpio de su capa sin exporner el DTO de aplicacion
adicinalmente me da la posibilidad de aplicar validaciones con Fluent


## 🚀 **Cómo Ejecutar el Proyecto**

### 1. Clonar el repositorio

```bash
git clone **RepoUrl**
```

### 2. Ejecutar los script de base de datos
Ubicados en `/Database`:
```
01 - CreateDatabase.sql
02 - Create Tables.sql
03 - Create Store Procedures.sql
04 - Insert Tests Accounts.sql
05 - Create Sequence.sql

```

### 2. Actualizar cadena de conexión
En `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=TechnicalInterviewDb;Trusted_Connection=True;"
}
```

### 3. Ubicarse en la carpeta del proyecto 
Remplazar **Username** por el usuario de la maquina **considerar modificar la ruta si clono el repositorio en otra carpeta**
``` bash
cd C:\Users\Username\source\repos\TechnicalInterviewEduardo\TechnicalInterview\TechnicalInterview 
```

### 4. Complilar el proyecto 
```bash
dotnet build 
```

### 4. Ejecutar API
```bash
dotnet run 
```

### 4. Abrir Swagger
```
http://localhost:5093/swagger/index.htm
```

### 5. Opcional Abrir Scalar 
```
http://localhost:5093/redoc/index.html
```


### 6. Opcional Ejecutar con postman
Ubicado en `/PostmanCollection`:
```
TechnicalInterviewMM.postman_collection.json
```


## 📌 **Endpoints Implementados**

### 1. Consultar Cuenta
```
Get /api/v1/Accounts/{accountId}
```

### 2. Crear Deposito
```
POST /api/v1/Accounts/{accountId}/deposit
```
Body:
```json
{
  "amount": 500,
  "description": "Test Deposit"
}
```

### 3. Crear Retiro
```
POST /api/v1/Accounts/{accountId}/withdrawal
```
Body:
```json
{
  "amount": 1600,
  "description": "Test Withdrawal"
}
```

### 4. Transferencia entre cuenta
```
POST /api/v1/Transfers
```
Respuesta:
```json
{
  "fromAccountId": "123456789",
  "toAccountId": "987654321",
  "amount": 10,
  "description": "Test Transfer"
}
```