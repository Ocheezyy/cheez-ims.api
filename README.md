# **Cheez IMS API** 🧀
An inventory management system built with **C# .NET 8.0**, using **Entity Framework Core** with **Npgsql** for PostgreSQL.

## **Features**
✅ RESTful API with Controllers  
✅ PostgreSQL database support  
✅ Entity Framework Core for ORM  
✅ CRUD operations for inventory management  
✅ Docker support for local development

## **Requirements**
- [Docker](https://www.docker.com/get-started)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## **Getting Started**

### **1. Clone the Repository**
```sh  
git clone https://github.com/Ocheezyy/cheez-ims.api.git  
cd cheez-ims-api  
```

### **2. Set Up Environment Variables**
Create a `.env` file in the project root and configure the database connection string:
```sh  
DATABASE_URL=postgres://url-here
```

### **3. Run with Docker**
Ensure Docker is running, then build and start the services:
```sh  
docker-compose up --build  
```
This will start both the API and a PostgreSQL database instance.

### **4. Apply Migrations**
If running for the first time, open a terminal inside the container and apply migrations:
```sh  
dotnet-ef database update  
```

[//]: # (### **5. API Endpoints**)

[//]: # (| Method | Endpoint             | Description                |  )

[//]: # (|--------|----------------------|----------------------------|  )

[//]: # (| GET    | `/api/items`         | Get all inventory items   |  )

[//]: # (| GET    | `/api/items/{id}`    | Get a single item         |  )

[//]: # (| POST   | `/api/items`         | Add a new item            |  )

[//]: # (| PUT    | `/api/items/{id}`    | Update an item            |  )

[//]: # (| DELETE | `/api/items/{id}`    | Remove an item            |  )

Test the API using [Postman](https://www.postman.com/) or `curl`. 
It also has swagger setup if you prefer using that.

### **6. Stopping the Services**
To stop and remove containers, use:
```sh  
docker-compose down  
```

## **Contributing**
1. Fork the repository.
2. Create a new branch (`feature/my-feature`).
3. Commit your changes and push them.
4. Open a pull request.

## **License**
See [License](https://github.com/Ocheezyy/cheez-ims.api/LICENSE.txt)

