# StarboxNet8CoreApiEF

This is part of a code challange to build a barista application. 

It was for developers applying for a job at my previous company. 

I have used it to practice and to learn technology, such as Angular. 

This version uses Sql Server, Entity Framework and .NET WebApi.

The scripts to create the database tables and to populate them are in the Scripts folder.
\StarboxLibraryNet8EF\Scripts\starbox-create-tables.sql
\StarboxLibraryNet8EF\Scripts\starbox-populate-tables.sql

I used database first rather than model first approach to Entity Framework.

This is the command I used to generate the DbContext class and the model classes.
PM> Scaffold-DbContext "Server=YOURSERVER;Database=StarboxDB;Trusted_Connection=True;TrustServerCertificate=True;"  Microsoft.EntityFrameworkCore.SqlServer -f

The repository pattern is straightfoward and the service layer is for business logic.

I use Dtos to keep the front end code from being dependent on the database entities.

I have a Angular front end that can be found at the following repository:
https://github.com/dougbean/starbox-angular-io-2025
