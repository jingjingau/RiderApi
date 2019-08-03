RiderApi Solution: 

1.	This Solution was developed in Visual Studio 2019 with .Net Core 2.2,  Entity Framework core 2.2, MVC 6, xUnit,  Moq,  SQL Server Express LocalDB,  ASP.NET REST WEB API,  C#,  Json,  .Net Core logging,  Jquery,  Javascript,  Html,  BootStrap

2.	There are 2 projects in this solution.
RiderApi: The REST Web API Service project with 2 web pages for test the APIs. 
RiderApi.Tests: The Unit Test Project for RiderApi project. 

3.	When the RiderApi project is run for the first time, RiderApi Database will be created in SQL Server Express LocalDB by code first, then some sample Riders and Jobs data are inserted into database.

    The setting for the DataBase connecting string is located in appsettings.json file.

4.	GitHub Source Code Link:
https://github.com/jingjingau/RiderApi

5.	Azure Deploy WebSite Link:
https://riderapi.azurewebsites.net/
