# CMPG 323 Project 3

**<ins>How to use:</ins>**<br/>
<ul>
  <li>To access Web Application: https://cmpg323project3-dev.azurewebsites.net</li>
  <li>When using the application you firstly need to register and then log in. Once you are logged in you can access all the methods, either through the links provided on the interface or the endpoints.</li>
  <li><strong>Background:</strong> This project implements Repository Patterns to create a abstraction layer between the data access layer and the business logic layer of an application. There is a generic repository and interface, where the controllers access the generic data access methods, and a repository and interface for each controller that contains the non-generic methods spesific to that controller.</li>
</ul>

**<ins>Endpoints:</ins>**
<ul>
  <li><strong>Customers:</strong></li>
  <ul>
    <li>/Customers</li>
    <li>/Customers/</li>
    <li>/Customers/Details/{id}</li>
    <li>/Customers/Create</li>
    <li>/Customers/Edit/{id}</li>
    <li>/Customers/Delete/{id}</li>
  </ul>
  <li><strong>Orders:</strong></li>
  <ul>
    <li>/Orders</li>
    <li>/Orders/OldOrders</li>
    <li>/Orders/Details/{id}</li>
    <li>/Orders/Create</li>
    <li>/Orders/Edit/{id}</li>
    <li>/Orders/Delete/{id}</li>
  </ul>
  <li><strong>Products:</strong></li>
  <ul>
    <li>/Products</li>
    <li>/Products/Stock</li>
    <li>/Products/Details/{id}</li>
    <li>/Products/Create</li>
    <li>/Products/Edit/{id}</li>
    <li>/Products/Delete/{id}</li>
  </ul>
</ul>


**<ins>Reference List:</ins>**
<ul>
  <li> Dukstra, T. 2022. <em>Implementing the Repository and Unit of Work Patterns in an ASP.NET MVC Application (9 of 10).</em> https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#the-repository-and-unit-of-work-patterns Date of access: 15 Sept. 2023.</li>
  <li>Hilton, J. 2018. <em>Pre-populate a textbox using ASP.NET Core MVC. </em> https://jonhilton.net/2018/02/05/pre-populate-a-textbox-using-asp.net-core-mvc/ Date of access: 17 Sept. 2023.</li>
  <li> Jaybhay, S. 2020. <em>Working With Multiple Tables In MVC By Sagar Jaybhay. </em> https://medium.com/@sagar_jaybhay/working-with-multiple-tables-in-mvc-by-sagar-jaybhay-912c5c161892 Date of access: 16 Sept. 2023. </li>
  <li> Kanjilal, J. 2015. <em> Implementing the Single Responsibility Principle in C#. </em> https://www.infoworld.com/article/2946023/implementing-the-single-responsibility-principle-in-c.html Date of access: 18 Sept. 2023.</li>
  <li> Khan, M. 2016. <em> Display data in Single View from Multiple Tables in ASP.Net MVC</em> https://www.aspsnippets.com/Articles/Display-data-in-Single-View-from-Multiple-Tables-in-ASPNet-MVC.aspx Date of access: 16 Sept. 2023.</li>
  <li> Kılıçarslan, S. 2019. <em>Repository Pattern Implementation in ASP.NET Core. </em> https://medium.com/net-core/repository-pattern-implementation-in-asp-net-core-21e01c6664d7 Date of access: 15 Sept. 2023.</li>
  <li> Microsoft. 2023. <em>Use ViewData and Implement ViewModel Classes. </em> https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/nerddinner/use-viewdata-and-implement-viewmodel-classes Date of access: 17 Sept. 2023</li>
  <li> Siddique, N. 2020. <em>What is ViewData in ASP .Net MVC C#?</em> https://www.tutorialspoint.com/what-is-viewdata-in-asp-net-mvc-chash#:~:text=ViewData%20is%20a%20dictionary%20of,view%2C%20not%20vice%2Dversa. Date of access: 17 Sept. 2023</li>
  <li>Spasojevic, M. 2022. <em>SOLID Principles in C# – Open Closed Principle. </em> https://code-maze.com/open-closed-principle/ Date of access: 19 Sept. 2023.</li>
  <li> TEKTUTORIALSHUB. 2020.<em>Entity Framework Include method</em> https://www.tektutorialshub.com/entity-framework/entity-framework-include-method/ Date of access: 17 Sept. 2023.</li>
</ul>

