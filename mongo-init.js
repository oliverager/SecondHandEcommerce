﻿// mongo-init.js
db = db.getSiblingDB("admin");

rs.initiate({
    _id: "rs0",
    members: [
        { _id: 0, host: "mongo:27017" }
    ]
});
