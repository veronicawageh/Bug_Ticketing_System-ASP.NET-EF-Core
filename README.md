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
- POST	/api/projects	Create a new project
- GET	/api/projects	Get a list of all projects
- GET	/api/projects/:id	Get details of a specific project and its bugs
- POST	/api/bugs	Report a new bug
- GET	/api/bugs	Retrieve a list of all bugs
- GET	/api/bugs/:id	View detailed information of a bug
- POST	/api/bugs/:id/assignees	Assign a user to a bug
- DELETE	/api/bugs/:id/assignees/:userId	Remove (unassign) a user from a bug
- POST	/api/bugs/:id/attachments	Upload an attachment to a specific bug
- GET	/api/bugs/:id/attachments	Get all attachments for a specific bug
- DELETE	/api/bugs/:id/attachments/:attachmentId	Delete an attachment from a specific bug
