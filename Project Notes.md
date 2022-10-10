# Dotnet Project
The dotnet project is a .NET 5.0 MVC application. It uses rudimentary dependency injection, and also includes a Swagger UI page.
The application has an injected database connection and configuration. The endpoints are mapped using the controller. There are two controllers in the application, 
`MotionPicture` and `MotionPictures`. `MotionPicture` contains CRUD operations for an individual `MotionPicture`. `MotionPictures` contains only a `GET` method for all motion pictures. 

The project uses Dapper for querying the database.

# Vue Project

The Vue project was created using the npm cli. Under the [components folder](./MotionPictureDataMangement.Vue/motion-picture-management/src/components) are the components for the application, including a table row, the movie form. 

The table row emits an event upon removal to the parent that the parent then listens to using the `@removed` syntax. Table rows are rendered in the table component using the `v-for` template binding.

Routing is implemented in the project using the `vue-router` project. Routes are implemented using the path parameters. Path parameters are accessed in the components using the router. Router links are used to route to specific routes.