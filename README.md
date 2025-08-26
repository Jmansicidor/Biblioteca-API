📚 Biblioteca API

* Proyecto de ASP.NET Core Web API (.NET 9) que implementa un sistema de gestión de Autores y Libros con relación uno a muchos:

Un Autor puede tener varios Libros.

Un Libro pertenece a un único Autor.

* El objetivo del proyecto es mostrar dos enfoques distintos para manejar DTOs (Data Transfer Objects):

    *  Rama con dto-manual:
         Implementa los DTOs y el mapeo de manera manual.
         Métodos de extensión (ToDto, ToEntity, UpdateEntity).

  *  Rama donde se utiliza mapster:
       Implementa los DTOs usando la librería Mapster que permite configuraciones más limpias, menos código repetitivo y mejor escalabilidad.
     
🚀 Tecnologías utilizadas
.NET 9
Entity Framework Core
Mapster
 (solo en rama mapster)
SQL Server
Swagger
 para documentación de endpoints

⚡ Endpoints principales

Autores:

GET /api/autores → Lista de autores con sus libros

GET /api/autores/{id} → Un autor específico

POST /api/autores → Crear un autor

PUT /api/autores/{id} → Actualizar un autor

DELETE /api/autores/{id} → Eliminar un autor

Libros:

GET /api/libros → Lista de libros

GET /api/libros/{id} → Un libro con datos de su autor

POST /api/libros → Crear un libro

PUT /api/libros/{id} → Actualizar un libro

DELETE /api/libros/{id} → Eliminar un libro
