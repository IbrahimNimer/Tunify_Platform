# Tunify Platform

## Overview
Tunify Platform is a web application designed to manage music-related entities, such as users, subscriptions, playlists, songs, artists, and albums. The platform integrates with a SQL Server database to store and manage data, reflecting the relationships between these entities.

## The Tunify ERD Diagram
![Tunify](https://github.com/user-attachments/assets/e68fdd72-7642-43f3-b92a-0bfe9a1cd843)

### Entity Relationships
- **User**: Can have multiple subscriptions and create multiple playlists.
- **Subscription**: Belongs to a user and can include various subscription plans.
- **Playlist**: Created by a user, contains multiple songs, and can be shared.
- **Song**: Can belong to multiple playlists and albums, and is associated with an artist.
- **Artist**: Can produce multiple albums and songs.
- **Album**: Contains multiple songs and is associated with an artist.
- **PlaylistSongs**: A join table that establishes a many-to-many relationship between playlists and songs, enabling a song to be in multiple playlists and a playlist to contain multiple songs.

## Repository Design Pattern

### Overview
The Repository Design Pattern is a structural pattern that helps to separate the data access logic from the business logic in an application. By using repositories, the data access code is encapsulated within a set of classes that are responsible for handling operations related to a specific entity or set of entities. This approach makes the application more modular, testable, and maintainable.

### Benefits of the Repository Pattern

1. **Separation of Concerns**: By encapsulating the data access logic within repository classes, the business logic is separated from the data access layer. This separation makes the application easier to manage and understand.

2. **Testability**: The use of interfaces in the Repository Pattern allows for easy mocking of the data access layer during unit testing. This means you can test the business logic without needing to interact with the actual database.

3. **Modularity**: Repositories provide a modular structure for the data access code. Each repository class is responsible for a specific entity, which makes it easier to manage and update the data access logic for that entity.

4. **Code Reusability**: By creating generic methods in the repository, you can reuse the same methods for multiple entities. This reduces code duplication and makes the codebase more maintainable.

5. **Centralized Data Access Logic**: All data access code is centralized in the repository classes, making it easier to implement changes in the data access layer without affecting the rest of the application.

### Implementation in Tunify Platform
In this lab, the Repository Design Pattern was integrated into the Tunify Platform to manage data access for entities such as `Users`, `Playlist`, `Song`, and `Artist`. Each entity has a corresponding repository interface and implementation class that encapsulates the data access logic.

- **Repository Interfaces**: These interfaces define the contract for data access operations, including CRUD operations and any entity-specific methods.

- **Repository Implementations**: The implementation classes provide the actual data access logic, interacting with the `DbContext` to perform operations on the database.

- **Controller Refactoring**: Controllers were refactored to use repositories instead of interacting directly with the `DbContext`. This refactoring improves the modularity and testability of the application.

By adopting the Repository Pattern, the Tunify Platform has become more modular, easier to test, and more maintainable, with a clear separation between business logic and data access logic.


# Swagger UI Integration
## Overview
### Swagger UI has been integrated into this project to provide an interactive interface for developers and testers to explore and interact with the Tunify Platform API. Swagger UI allows you to see the API documentation, understand the endpoints, and execute requests directly from the browser without the need for third-party tools.
## Key Features of Swagger UI
### -Interactive API Documentation: Easily browse and test all the API endpoints with detailed information about request parameters and responses.
### -Live Request Testing: Make API requests directly from the browser and see live responses.
### -Customizable: The Swagger UI can be customized to suit the needs of your project, including setting custom paths, titles, and descriptions.
## How to Access and Use Swagger UI
### Start the Application.
### Open Swagger UI:
###     -Open your web browser.
### Explore the API:
### View and test all available API endpoints directly from the Swagger UI.
# Tunify Platform - User Authentication Service
### This repository includes the implementation of a user authentication service for the Tunify Platform, utilizing ASP.NET Core Identity for user management. The service is designed to handle user registration, login, and logout functionalities.
## Overview
### The project is structured around the following components:
## 1. IdentityUserService
### Location: Repositories/Services/IdentityUserService.cs
Purpose: This class implements the IUser interface to provide user management functionalities such as registration, login, and logout using ASP.NET Core Identity.
Key Methods:
Register: Registers a new user by creating an instance of ApplicationUser. Handles potential errors during registration and returns the newly created user's details.
LoginUser: Authenticates a user by validating their credentials and returns the user's details if the credentials are correct.
LogoutUser: Logs out the user by signing them out and returns their details.
## 2. IUser Interface
Location: Repositories/Interfaces/IUser.cs
Purpose: This interface defines the contract for user management operations. It ensures that the IdentityUserService implements the required methods for registering, logging in, and logging out users.
## 3. HomeController
Location: Controllers/HomeController.cs
Purpose: This API controller provides endpoints for user registration, login, and logout. It interacts with the IdentityUserService to perform these operations.
API Endpoints:
POST /api/home/register: Registers a new user.
Request Body: RegisterUserDTO containing UserName, Email, and Password.
Response: Returns the user details if successful, otherwise returns validation errors.
POST /api/home/login: Logs in a user.
Request Body: LoginDto containing Username and Password.
Response: Returns the user details if successful, otherwise returns an Unauthorized status.
POST /api/home/logout: Logs out a user.
Request Body: A string representing the Username.
Response: Returns the user details after successful logout.
## DTOs (Data Transfer Objects)
### RegisterUserDTO
Represents the data required to register a new user.
Fields:
UserName
Email
Password
### LoginDto
Represents the data required to log in a user.
Fields:
Username
Password
### UserDto
Represents the user details returned after registration, login, or logout.
Fields:
Id
UserName
## How to Use
### Registration:
Send a POST request to /api/home/register with the required user details.
Login:
Send a POST request to /api/home/login with the user's credentials.
Logout:
Send a POST request to /api/home/logout with the username to log out the user.

