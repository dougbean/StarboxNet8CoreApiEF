# StarboxNet8CoreApiEF

This is the back end part of a simple barista or drink dispensing application. It is written on the .NET Framework 8.0. It uses WebAPI, Entity Framework and Sql Server.

I have two front ends, one written with Angular and one with React. They can be found at the following urls.
https://github.com/dougbean/starbox-angular-io-2025 <br>
https://github.com/dougbean/starbox-reactjs-nextjs

The requirements document is here - https://github.com/dougbean/StarboxNet8CoreApiEF/blob/main/StarboxLibraryNet8EF/Docs/Starbox.pdf

I followed the requirements more or less, though I used .NET for the back end rather than Ruby on Rails or Groovy on Grails.

The requirements document is a code challange presented to job applications of my former employer. I did it to practice and to learn new technologies, such as Angular and React; and to refresh my .NET skills. I strive to follow best practices.

The scripts to create the database tables and to populate them are in the Scripts folder. <br>
\StarboxLibraryNet8EF\Scripts\starbox-create-tables.sql <br>
\StarboxLibraryNet8EF\Scripts\starbox-populate-tables.sql

I used database first rather than model first approach to Entity Framework.

This is the command I used to generate the DbContext class and the model classes. PM> Scaffold-DbContext "Server=YOURSERVER;Database=StarboxDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -f

The repository pattern is straightfoward and the service layer is for business logic.

I use Dtos to keep the front end code from being dependent on the database entities.
