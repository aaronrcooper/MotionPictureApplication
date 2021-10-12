# Project Structure

The project is composed of three main folders:
1. MotionPictureDataManagement - Contains the ASP.NET Core API application
2. MotionPictureDataManagement.Web - Contains the Vue.js application
3. Scripts - Contains the database script

# Running the Applications
The application can be ran using a MySql Docker Image by installing the Docker Command Line tools and running 

```
docker-compose up -d
```

from the [root](./) of the repository.

Using these default settings in the Docker file, the connection string for the dotnet project will be:
`Server=localhost;port=3306;Database=MotionPicture;Uid=root;Pwd=password`
This setting can be found in [appsettings](./MotionPictureDataManagement/appsettings.Development.json)

## Running the API
The following command will start the application from the root of the project
```
dotnet run -p MotionPictureDataManagement
```

### Swagger
Included in the project is a [Swagger UI view.](https://localhost:5001/api/docs)

It will be deployed under the `/api/docs` path when you launch your application. The above link uses the dotnet default port.

### Prerequisites
- [Dotnet SDK](https://dotnet.microsoft.com/download)


## Running the Vue.js Application

Prior to running the application, you must install all dependencies by navigating to [the Vue project](./MotionPictureDataManagement.Vue/motion-picture-management) and run 
the `npm install` command.

After downloading the dependencies, navigate to the `MotionPictureDataManagement.Vue/motion-picture-management` and run the command `npm run serve`.

### Prerequisites
- [NPM](https://nodejs.org/en/)