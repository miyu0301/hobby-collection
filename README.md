# Hobby Collection

This application is built with ASP.NET Core and has secure login authentication using Identity. 
This supports basic CRUD (Create, Read, Update, Delete) operations and image storage, allowing users to manage their data effectively.  
Users can store information about items that you're interested in. Enjoy expanding your collection of favorites!
  

## Built With

- **ASP.NET Core**
- **Entity Framework Core**
- **Razor Pages**
- **Identity**
- **SQLite**
- **Cloudinary**
  

## App Images

<div align="center">
    <img src="/readme/app-image-list.png" alt="favorite-list" width="500"> <img src="/readme/app-image-create.png" alt="favorite-create" width="500">
</div>
  

##  How to Install

1. Clone the repository to your local machine using Git
```
git clone https://github.com/miyu0301/hobby-collection.git
```

2. You need to create a `.env` file in the root directory of the project and add the following settings:
```
{
  "CloudinarySettings": {
    "CloudName": "your_cloud_name",
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
  }
}
```
Replace `your_cloud_name`, `your_api_key`, and `your_api_secret` with your actual Cloudinary account details.

3. Execute the following commands to restore dependencies, update the database, build the project, and run it:
```
dotnet restore
dotnet ef database update
dotnet build
dotnet run
```

4. Finally, open your web browser and navigate to the following address to access the application:
```
http://localhost:5066/
```

