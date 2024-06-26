# Product Management System

## Project Overview
- **Description**: This is a project for managing customers, items, and the assignment between them. Additionally, it includes options to generate various types of reports.

- **Features**:
  - Create, update, and disable customers.
  - Create, update, and disable items.
  - Assign items to customers.
  - Generate report of items and the customers assigned to them.
  - Generate report of most expensive item(s) per customer.

- **Technologies Used**:
  - **Front-end**: Blazor WebAssembly
  - **Back-end**: ASP.NET 8 Web API
  - **SQL Engine**: SQL Server
  - **Authentication Server**: Azure Active Directory
  - **Reporting Tool**: Quest
  - **Most Important Dependencies**: Entity Framework Core, FluentValidations, MSAL, AutoMapper, SweetAlert2...


## Installation and Setup
Before you spend time setting up the entire project, consider test it  through the following link. The application is deployed, and you can log in using the following credentials.

[Click here to visit the app](productmanagementfrontend.azurewebsites.net) 🌐

**👨‍🦲Test user 1 : **
- Email: `ivan@angelsebastianbellomateohot.onmicrosoft.com`
- Password: `Eladio3456`

**🧔Test user 2:** 
- Email: `saiter@angelsebastianbellomateohot.onmicrosoft.com`
- Password: `Eladio3456`

To access the application and utilize its various functionalities, you must be an authenticated user. All users have access to the same base of customers and items. The first time you access the application link, a Microsoft pop-up will appear prompting you to enter an email and password. Currently, to add a user to the application, you need to contact the system ADMIN who will provide you with an email and password. After the initial login, the same pop-up will prompt you to change your password.


### Setup app locally - prerequisites 


*-Sql Server Management Studio 19 installed or any other database client with access to Sql Server engine (E.g. DBeaver, Azure Data Studio).*

*-.NET 8 runtime installed in your local machine.*

*-Preferibly, Visual Studio 2022 as your IDE, but you can any of your choice.*

*-Clone the repository from `git clone  https://github.com/AngelSBM/ProductManagementSystem `  *

### Database setup
The project uses database-first approach, so migrations are not used. Instead, you need to execute a script that creates the database with tables and some initial data to populate them.

First, open *SQL Server Management Studio (SSMS)* and connect to your local server. 

Navigate to the project directory and then `/Scripts`, here you will find the file `ProductManagementDB.sql` open this file or copy its content and paste it in SSMS and execute the script.

Verify that database with the name of **ProductManagementDB** has been created

### Backend Setup
Navigate to the project directory and open **ProductManagementSystem.sln**, this is the solution which contains the back-end and the front-end as well. Open the solution in Visual Studio. Right click the solution icon, click on "Clean solution" and the right click again and click on "Rebuild solution"

Navigate to `ProductManagementSystem.Backend/Presentation/appsettings.json`

In this configuration file, you will change the connection string from the the cloud server (hosted in azure) to your local server. The key you must change is **ProductManagementDB**

Paste this value as your new connection string: 

`data source=localhost;initial catalog=ProductManagementDB;trusted_connection=true; TrustServerCertificate=True`

Right click the `ProductManagementSystem.Backend/Presentation`  project in the solution and click on "Debug => start new instance"

Copy the url of the server where our API is served by Visual Studio (E.g. https://localhost:44307) and paste it in a clipboard.

### Frontend Setup

Navigate to `
ProductManagementSystem.Frontend/ProductManagementSystem.Frontend/wwwroot/appsettings.json
`
In this configuration file you will change the **BaseUrl** key from the cloud hosted web api to the api that we just runned in **Backend Setup** section, paste the url of the api locally runned previously  (E.g. https://localhost:44307).

### Final setup - running the app
Verify that the solution is configured to run multiple startup projects, in this case, the projects are **Presentation** and **ProductManagementSystem.Frontend**

Run the solution, front-end and back-end will start running. 🔥🔥🔥

## Project Structure
- Layered Architecture
- Repository Pattern
- Unit of Work pattern
- Dtos patern
- Rich Domain (DDD)


## Troubleshooting
-Contact the owner through these channels:
- email: angelsebastianbellomateo[arroba]gmail.com
- linkedIn: https://www.linkedin.com/in/angel-s-bello-957634204/

## Licensing and Credits
Ángel Bello 
