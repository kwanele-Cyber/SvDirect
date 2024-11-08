# Users Rest API

The REST APIdefines operations exposed by the micriservice

| Operation | Description |
|--|--|
| GET /users | Retrives all users |
| GET /users/{id} | Retrives a specified user by their id |
| POST /users | Create a user |
| PUT /users/{id} | Modifies the specified user |
| DELETE /users/{id} | Deletes the specified user |

## Data Transfer Object (Dtos)

A DTO represents the **contract** between the microservice API and the client.

## Record Types in C\#

```c#
    private record UserDto = new record(string id, string name, string lastname);
```

### Why choose record types?

- Simpler to declare.
- Value-based equality
- Immutability by default.
- built-in ToString() override
