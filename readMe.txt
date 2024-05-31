AgriEnergySolution Web Application
----------------------------------
Overview
This is an ASP.NET Core MVC web application built using Visual Studio. It is designed to manage information about farmers and their products, with distinct functionalities for two types of users: Farmers and Employees.

Login Details for web application 
-
## Employee Login:
Email: dave@gmail.com
Password: Pass@1234

Farmer Login:
All farmer profile login in details can be accessed through the Employee Login.


Table of Contents
-----------------
1. Setup Instructions
1.1 Visual Studio setup
1.2 Database Configuration
2. Building and Running the Application
3. System Functionalities and User Roles
4. Detailed Functionality
4.1 Farmers
4.2 Employees
5. Authentication and Security

1.Setup Instructions
-
## 1.1 - Visual studio setup
Prerequisites
Visual Studio 2019/2022: Ensure you have Visual Studio installed with the following workloads:

ASP.NET and web development
.NET Core cross-platform development
SQL Server Management Studio (SSMS): For managing the relational database.

Extracting the Project
Extract the ZIP File:

Locate the zip file that contains the project.
Right-click on the zip file and select "Extract All..." to unzip the contents to a folder of your choice.
Open the Solution:

Open Visual Studio.
Click on "Open a project or solution".
Navigate to the extracted folder and select AgriEnergySolution.sln.
Setting Up the Development Environment
Restore NuGet Packages:

Visual Studio will automatically restore the necessary NuGet packages on solution load.
If it doesn't, right-click on the solution in Solution Explorer and select "Restore NuGet Packages".

## 1.2 Database Configuration

## Database Backup and Restore Instructions

### Prerequisites

- Ensure that SQL Server is installed and running on your machine.
- Download and extract the database backup file provided (`AgriSolution.bak`).

### Restoring the Database

1. **Open SQL Server Management Studio (SSMS)**:
   - Open SSMS and connect to your SQL Server instance.

2. **Restore the Database**:
   - In the Object Explorer, right-click on `Databases`.
   - Select `Restore Database...`.
   - In the Restore Database window:
     - Select `Device` and click on the ellipsis (`...`) to browse for the backup file.
     - Add the path to the `AgriEnergySolutionDB.bak` file.
     - Ensure the destination database name is set to `AgriEnergySolutionDB`.
     - Click `OK` to restore the database.

3. **Update Connection String**:
   - Open the `appsettings.json` file located in the root of the project directory.
   - Update the `DefaultConnection` string with the details of your SQL Server instance.

   ```json
  
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AgriSolution;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true"
  }




2.Building and Running the Application
-
Building the Application
Click on "Build" in the top menu and select "Build Solution" or press Ctrl+Shift+B.
Running the Application
To run the application, press F5 or click the "IIS Express" button in the toolbar.
The application will start, and your default browser will open with the application's home page.


3.System Functionalities and User Roles
-
The system has two main user roles: Farmers and Employees.

## Farmers
Farmers can log in using credentials created by Employees.
They have the ability to add new products with details such as name, category, and production date.
Farmers can view and manage their own product listings.

## Employees
Employees can add new farmer profiles with details such as name, email, and password.
Employees can view all products from specific farmers and use filters to search products by date range and product type.
Employees can add new employee profiles with essential details such as name, email, and password.

4.Detailed Functionality
-
## 4.1 Farmers
Add Products: Farmers can add products to their profiles. The required details include:
Product Name,
Category and,
Production Date.
View Products: Farmers can view a list of all the products they have added.

## 4.2 Employees
Add Farmer Profiles: Employees can create new farmer profiles by providing the following details:
Name,
Email and,
Password.

View and Filter Products: Employees can view products from all farmers and apply filters based on:
Date Range and,
Product Type.
Add Employee Profiles: Employees can create new employee profiles with the following details:
Name,
Email and,
Password.

5.Authentication and Security
-
Secure Login: The application implements secure login functionality using ASP.NET Core Identity.
Role-Based Access Control: Access to different parts of the system is controlled based on user roles (Farmer and Employee).
Password Hashing: Passwords are hashed before storing them in the database to ensure security.