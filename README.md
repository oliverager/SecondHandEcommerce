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

## 🧠 Design Decisions

- **MongoDB** is used for flexible schema and scalability.
- **Redis** caches read-heavy data like listings for fast access.
- **CQRS** separates write (commands) and read (queries) paths.
- **Repositories** abstract Mongo logic, keeping domain clean.
- **GUIDs** are used for `Id` to decouple from MongoDB’s `ObjectId`.

---

## 👥 Team Contributions

Each group member contributed to:
- Architecture and domain modeling
- CQRS layer and command/query handlers
- MongoDB/Redis setup and integration
- Dockerization and environment setup
- API design and controller logic

Git history clearly reflects individual contributions.

---

## 📌 TODO / Extensions

- Implement cloud storage integration for images
- Add seller reviews and user rating system
- Handle transaction consistency in multi-step operations (e.g., place order + reserve listing)
- Implement advanced search and filtering

---

## 📄 License

MIT