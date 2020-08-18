# cognizant-challenges-api #

.Net + Angular implementation of the cognizant challanges task.

![alt text](https://i.ibb.co/LhMPnvr/1.png)
![alt text](https://i.ibb.co/QFPvxfK/2.png)

## Table of Contents

- [Installation](#installation)
- [Remarks](#remarks)

## Installation 

In order to run the application, first you have to boot up PgSql server with the password `admin` and then copy the content of `src/Cognizant.Infrastructure/Data/PgSql/Challanges/ChallengesDB.sql` to the query.
Then, simply boot up the project with `Cognizant.ChallangesApi` as startup project and run with IIS Express.

### Prerequisites

1. Required:
    * [.NET Core 3.1.*](https://dotnet.microsoft.com/download/dotnet-core/3.1) - Hosting boundle runtime for hosting the backend service
    * [PostgreSql](https://www.postgresql.org) - Relational database
    
2. Optional:
    * [IIS Express](https://docs.microsoft.com/en-us/iis/extensions/introduction-to-iis-express/iis-express-overview) - Lightweight, self-contained version of IIS optimized for developers
    * Development IDE
        * [Visual Studio](https://visualstudio.microsoft.com/downloads)
        * [Visual Studio Code](https://code.visualstudio.com/)
    * [pgAdmin](https://www.pgadmin.org) - Open Source administration and development platform for PostgreSQL
    
## Remarks 

What I would do if there was more time and resources for further development of this solution:
- separate the angular project from dotnet and containerize both of them.
- more testsing.
- use ef migrations for easier setting up of databases.
- add more annotations throughout the whole project.
- more detailed error handling.
- improve validators for user inputs.
- CI for project build validity, for example github actions.

Service architecture is loosely based on [Manga clean architecture template](https://github.com/ivanpaulovich/clean-architecture-manga)
