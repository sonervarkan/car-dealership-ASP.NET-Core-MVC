# Car Gallery ASP.NET Core MVC Project

A fully functional ASP.NET Core MVC car dealership application where users can browse cars, filter listings, view details, and see featured cars on the homepage slider.
The project uses Entity Framework Core, SQL Server, and a clean MVC architecture.

## Features
### Homepage

Dynamic image slider

Latest added featured cars

Popular cars section

“Add Car” button (future integration)

### Car Listing

Display all cars with:

Brand

Model

Image

Price

Year

Clean card layout with responsive design

### Car Filtering

Users can filter cars based on:

Brand

Model

Gear Type

Fuel Type

Year

Price Range

### Car Detail Page

Each car has its own detail page with:

Large image

Brand & model

Year

Price

Description

## Architecture

ASP.NET Core MVC pattern

EF Core for database access

Strongly typed views (Razor)

Partial views for reusable sections (Navbar, Footer, Slider)

## Tech Stack
````
Layer	Technology
Backend	ASP.NET Core MVC
Frontend	HTML, CSS, Bootstrap, Razor Views
Database	SQL Server + Entity Framework Core
Dependency Injection	Built-in .NET DI container
View Engine	Razor
````
## Project Structure
````
car-dealership-ASP.NET-Core-MVC/
│
├── Controllers/
│   ├── HomeController.cs
│   ├── CarController.cs
│   └── SessionController.cs
│
├── Models/
│   ├── Entities/
│   │   ├── Car.cs
│   │   ├── Images.cs
│   │   ├── Roles.cs
│   │   └── Users.cs
│   │
│   ├── CarViewModel.cs
│   ├── ErrorViewModel.cs
│   └── RegisterViewModel.cs
│
├── Data/
│   └── CarDealershipDbContext.cs
│
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml
│   │   ├── Filter.cshtml
│   │   └── Privacy.cshtml
│   │
│   ├── Car/
│   │   └── AddCar.cshtml
│   │
│   ├── Session/
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   │
│   └── Shared/
│       └── _Layout.cshtml
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── img/
│
├── appsettings.json
├── Program.cs

````

## Database Structure (EF Core)
````
Car Table
Column	    Type
Id	        int
Brand	     string
Model	     string
GearType    string
FuelType    string
Year	      int
Color       string
Price	      decimal
````
## Installation & Setup
1️⃣ Clone the repository
git clone https://github.com/yourrepo/car-gallery-aspnetmvc.git
cd car-gallery-aspnetmvc

2️⃣ Configure your database

Edit appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=CarGalleryDB;Trusted_Connection=True;"
}

3️⃣ Apply migrations
dotnet ef database update

4️⃣ Run the application
dotnet run


Open in browser:

https://localhost:5001


## Future Improvements

User authentication (Login / Register)

Admin panel

Car management (Add / Edit / Delete)

Multi-image upload

Favorites system

Pagination for large datasets

## License

This project currently has no license.
