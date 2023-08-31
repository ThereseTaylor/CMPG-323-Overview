# CMPG 323 Project 2

**<ins>How to use:</ins>**<br />
<ul>
  <li>To access API: https://cmpg323project2-dev.azurewebsites.net/<br /></li>
  <li><strong>Authentication:</strong></li>
    <ul>
      <li>Register if you are a new user. Password should adhere to good practices, make sure to store your password for later use. End point: /api/Authenticate/register./</li>
      <li>If you are registered then log in to access the rest of the system. End point: /api/Authenticate/login</li>
      <li>After you have logged in, you will receive a token in the response section, copy this token.</li>
      <li>Go to the Authorize button at the top of the page or select the lock on any method and paste the token in the textbox, in the following format: Bearer[space]"your token".</li>
      <li>After you are authorized you can access and execute any method.</li>
    </ul>
  <li>Methods:</li>
    <ul>
      <li>There is 5 entries in each table, thus if the method requires an id as input only 1-5 will return a result.</li>
      <li>Delete: you can not delete a record that is referenced in another table (as a foreign key), as the database restricts this. Thus only id: 4, 5 of Customers and Products can be used for testing purposes.</li>
      <li>Post: to add a new record to a spesific table, you have to remove all the content of the other tables shown in the api output, aswell as the last comma, before executing it.</li>
    </ul>
</ul>

**<ins>Endpoints:</ins>**
<ul> 
  <li><strong>Customers:</strong></li>
    <ul>
      <li>GET: /api/Customers</li>
      <li>GET: /api/Customers/{id}</li>
      <li>PUT: /api/Customers/{id}</li>
      <li>POST: /api/Customers</li>
      <li>DELETE: /api/Customers/{id}</li>
    </ul>
  <li><strong>Orders:</strong></li>
    <ul>
      <li>GET: /api/Orders</li>
      <li>GET: /api/Orders/int/{id}</li>
      <li>GET: /api/Orders/{customerid}</li>
      <li>PUT: /api/Orders/{id}</li>
      <li>POST: /api/Orders</li>
      <li>DELETE: /api/Orders/{id}</li>
    </ul>
  <li><strong>Products:</strong></li>
    <ul>
      <li>GET: /api/Products</li>
      <li>GET: /api/Products/int/{id}</li>
      <li>GET: /api/Products/{orderid}</li>
      <li>PUT: /api/Products/{id}</li>
      <li>POST: /api/Products</li>
      <li>DELETE: /api/Products/{id}</li>
    </ul>
  <li><strong>Order Details:</strong></li>
    <ul>
      <li>GET: /api/OrderDetails</li>
      <li>GET: /api/OrderDetails/{id}</li>
      <li>PUT: /api/OrderDetails/{id}</li>
      <li>POST: /api/OrderDetails</li>
      <li>DELETE: /api/OrderDetails/{id}</li>
    </ul>
</ul>

**<ins>Reference List:</ins>**<br />
<ul>
  <li>Anderson, R. 2023. <em>Controller action return types in ASP.NET Core web API.</em> https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-7.0 Date of access: 28 Aug. 2023.</li>
  <li>Anderson, R., Larkin, K., Nowak, R. 2023.<em>Routing in ASP.NET Core.</em> https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0 Date of access: 28 Aug. 2023</li>
  <li>Cone, M. 2023. <em>Basic Syntax.</em> https://www.markdownguide.org/basic-syntax/ Date of access: 30 Aug. 2023. </li>
  <li>Entity Framework Tutorial. n.d. <em>Querying in Entity Framework Core.</em> https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx Date of access: 26 Aug 2023.</li>
  <li>Jagger, M. 2023. <em>What is the Difference Between PUT and PATCH?.</em> https://www.abstractapi.com/guides/put-vs-patch#:~:text=A%20PATCH%20request%20sends%20data,%2C%20XML%2C%20or%20query%20parameters. Date of access: 29 Aug. 2023.</li>
  <li>Johnson, Tom. 2019. <em>Endpoints and methods (API reference tutorial).</em> https://idratherbewriting.com/learnapidoc/docapis_resource_endpoints.html Date of access: 30 Aug. 2023.</li>
  <li>Montenegro, O. 2023. <em>How to Use Patch Requests to Update Resources in Your REST API.</em> https://unitcoding.com/how-to-use-patch-requests-to-update-resources-in-your-rest-api/ Date of access: 29 Aug. 2023.</li>
  <li>Montenegro, O. 2023. <em>Exposing Related Entities in your Web API.</em> https://unitcoding.com/exposing-related-entitites/#:~:text=Exposing%20related%20entities%20in%20ASP,data%20in%20a%20meaningful%20way. Date of access: 26 Aug. 2023</li>
  <li>Muller, J. 2022. <em>Join two entities in .NET Core, using lambda and Entity Framework Core.</em> https://jd-bots.com/2022/01/24/join-two-entities-in-net-core-using-lambda-and-entity-framework-core/ Date of access: 29 Aug. 2023.</li>
   <li>Schults, C. 2021. <em> Git Delete Branch How-To, for Both Local and Remote.</em> https://www.cloudbees.com/blog/git-delete-branch-how-to-for-both-local-and-remote Date of access: 25 Aug. 2023.</li>
  <li>Wiltzer, M. 2022. <em> ASP.NET Core – The request matched multiple endpoints.</em> https://makolyte.com/aspnetcore-the-request-matched-multiple-endpoints/ Date of access: 26 Aug.2023.</li>
  <li>Yogi. 2019. <em> ASP.NET Core — How to use Dependency Injection in Entity Framework Core.</em> https://medium.com/hackernoon/asp-net-core-how-to-use-dependency-injection-in-entity-framework-core-4388fc5c148b#:~:text=Add%20DbContext%20class%20as%20a%20service%20in%20Startup.&text=Extensions.,object%20given%20in%20it's%20parameter.&text=This%20change%20will%20help%20you%20to%20read%20the%20appsettings. Date of access: 25 Aug. 2023.</li>
</ul>
