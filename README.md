# SecondHandEcommerce Backend

This project is the backend implementation of a second-hand e-commerce platform. It allows users to list, browse, and order second-hand items. Built with **.NET 9**, **MongoDB**, and **Redis**, the architecture follows best practices such as **CQRS**, **clean architecture**, and **cloud-native scalability**.

---

## 📦 Tech Stack

| Layer            | Technology            |
|------------------|------------------------|
| Backend Framework| ASP.NET Core (.NET 9) |
| Database         | MongoDB (NoSQL)       |
| Caching          | Redis                 |
| CQRS             | Application Layer     |
| Cloud Storage    | (To be integrated, e.g., AWS S3) |
| Containerization | Docker                |

---

## 📁 Project Structure

```
SecondHandEcommerce/
├── src/
│   ├── Api/                 # Web API (Controllers, Program.cs)
│   ├── Application/         # Commands, Queries, Handlers
│   ├── Domain/              # Entities, Value Objects
│   └── Infrastructure/      # MongoDB, Redis, Cloud Storage
├── docker-compose.yml       # Redis and Mongo containers
└── README.md
```

---

## ✅ Features

- Users can **create and view listings**
- Orders can be **created and tied to listings**
- Listings are **cached in Redis** for performance
- Cleanly separated **CQRS architecture**
- MongoDB integration with repository pattern
- Dockerized services for local development

---

## 🚀 Getting Started

### 1. Clone the Repo

```bash
git clone https://github.com/your-org/SecondHandEcommerce.git
cd SecondHandEcommerce
```

### 2. Start the Full Stack (MongoDB, Redis, API)

```bash
docker-compose up -d
```

This will:
- Start MongoDB in replica set mode on port 27017
- Launch Redis for caching
- Build and run the .NET API on port 5053

### 3. The API Is Running

Visit: [http://localhost:5053/swagger](http://localhost:5053/swagger)

---

## 🔁 API Overview

### `POST /api/listing`

Creates a new item listing.

### `GET /api/listing`

Returns all listings (cached in Redis).

### `GET /api/listing/{id}`

Returns a specific listing by ID.

### `POST /api/order`

Places a new order and marks the listing as reserved (transactional).

### `GET /api/order`

Returns all orders.

### `GET /api/order/{id}`

Returns a specific order by ID.

---

## 🧪 Testing

Use Swagger or Postman to send requests to:

```bash
http://localhost:5053/api/listing
```

Example request:

```json
{
  "title": "Gaming Laptop",
  "description": "RTX 3070, 16GB RAM",
  "price": 899.99,
  "sellerId": "user123",
  "category": ["electronics"],
  "imageUrls": ["https://image.com/laptop.jpg"]
}
```

---

🧠 Design Decisions

  **MongoDB** is used for flexible schema and scalability.

  **MongoDB Replica Sets**, we can ensure high availability and failover capabilities. This setup allows the system to handle a higher load and maintain availability even in case of primary node failures.


  **MongoDB Atlas:** While the system is running locally, we also considered hosting the database on MongoDB Atlas to provide a fully managed, scalable cloud solution. Atlas would automatically handle the database’s scaling, backup, and monitoring. With Atlas, we can set up global clusters and take advantage of multi-region replication, which is beneficial for global applications.

  **Redis caches** read-heavy data like listings, orders, and users for fast access, reducing database load and improving performance:

        Caching in GetOrderByIdQueryHandler: Redis is used to cache the result of retrieving an order by Id. The system first checks if the order is already cached before querying MongoDB. If not found in the cache, the order is fetched from the repository and cached for future requests.

        Caching in GetUserByIdQueryHandler: Similarly, user data is cached using Redis. When querying a user by UserId, the system checks Redis for the cached result, reducing the need for repeated database queries.

        More caching could be added in future, especially for stuff like search results if we had a more time to make the platform. 

  **CQRS:** The system uses the Command Query Responsibility Segregation pattern to separate read (queries) and write (commands) paths, ensuring better performance and maintainability.

  **Repositories:** MongoDB repositories are used to abstract away the database logic, keeping the domain layer clean and focused on business logic.

  **GUIDs:** All Id fields use GUIDs instead of MongoDB’s ObjectId for greater flexibility and consistency across systems.

  **Indexes:** Indexes are created on frequently queried fields, such as sellerId in Orders and Listings, to improve the performance of data retrieval.

list of indexes created

rs0 [direct: primary] test> use SecondHandEcommerce
switched to db SecondHandEcommerce

// Users Indexes
rs0 [direct: primary] SecondHandEcommerce> db.Users.getIndexes()
[
  { v: 2, key: { _id: 1 }, name: '_id_' },
  { v: 2, key: { email: 1 }, name: 'email_1', unique: true }
]

// Orders Indexes
rs0 [direct: primary] SecondHandEcommerce> db.Orders.getIndexes()
[
  { v: 2, key: { _id: 1 }, name: '_id_' },
  { v: 2, key: { buyerId: 1 }, name: 'buyerId_1', unique: true },
  { v: 2, key: { createdAt: 1 }, name: 'createdAt_1', unique: true }
]

// Listings Indexes
rs0 [direct: primary] SecondHandEcommerce> db.Listings.getIndexes()
[
  { v: 2, key: { _id: 1 }, name: '_id_' },
  { v: 2, key: { sellerId: 1 }, name: 'sellerId_1' }
]

// Reviews Indexes
rs0 [direct: primary] SecondHandEcommerce> db.Reviews.getIndexes()
[
  { v: 2, key: { _id: 1 }, name: '_id_' },
  { v: 2, key: { sellerId: 1 }, name: 'sellerId_1', unique: true }
]

---

## 👥 Team Contributions

Made by Alexander and Oliver

---



## 📄 License

MIT