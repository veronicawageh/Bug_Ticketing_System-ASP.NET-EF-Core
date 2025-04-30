# Bug Ticketing System API

A complete backend system for managing bug reports, user roles, projects, and file attachments — built using ASP.NET Core and Entity Framework Core.

##  Features

-  User registration and JWT-based authentication
-  Bug creation, assignment, and tracking
-  File uploads with support for attachments via `IFormFile`
-  Project management with bug grouping
-  Full Postman support for testing endpoints
-  FluentValidation for data integrity

  ---
#  Tech Stack

- ASP.NET Core 8
- Entity Framework Core (Code First)
- SQL Server
- JWT Authentication
- Identity & Claims
- FluentValidation
- Postman (for testing)
----
# Api Endpoints
- POST	/api/users/register	Register a new user account
- POST	/api/users/login	Authenticate user and return a JWT token
