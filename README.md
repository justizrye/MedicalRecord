This is an <b>ASP.NET Core Web API</b> project used to record medical records of employees. It uses <b>Dapper</b> libraries to connect to SQL Database. The database is located in folder <b>'MedicalRecord.Data'</b>. Please attach to the database first before using. Basic logging is provided using <b>Nlog</b> libraries. The logs will be generated in root folder of system drive (e.g. C:\MedicalRecord.Logs)

I used following for this project: Visual Studio 2019/2022, SQL Server 2019, Postman

These are the functions available for this project:

<b>Create</b><br>
Creates new employee record. Please make a <b>POST</b> request to: https://localhost:44389/api/employees or https://localhost:44389/api/employees-async with body:
  ```json
  {
    "id": 26,
    "firstName": "John",
    "lastName": "Appleseed",
    "temperature": 38,
    "createdDate": "2022-03-28T07:00:00Z",
    "updatedDate": "2022-03-28T07:00:00Z"
  }
  ```
<b>Update</b><br>
Updates existing employee record. Please make a <b>PUT</b> request to: https://localhost:44389/api/employees/{id} or https://localhost:44389/api/employees-async/{id} with body:
  ```json
  {
    "firstName": "Johnny",
    "lastName": "Appleseed",
    "temperature": 33,
    "createdDate": "2022-03-28T07:00:00Z",
    "updatedDate": "2022-03-28T07:10:00Z"
  }
  ```
<b>Delete</b><br>
Deletes existing employee record. Please make a <b>DELETE</b> request to: https://localhost:44389/api/employees/{id} or https://localhost:44389/api/employees-async/{id}.

<b>Get All</b><br>
Retrieves all employee records. Please make a <b>GET</b> request to: https://localhost:44389/api/employees or https://localhost:44389/api/employees-async.

<b>Get</b><br>
Retrieves an employee record. Please make a <b>GET</b> request to: https://localhost:44389/api/employees/{id} or https://localhost:44389/api/employees-async/{id}.

<b>Search</b><br>
Searches employee records using given criteria. Please make a <b>GET</b> request to: https://localhost:44389/api/search-employees/ or https://localhost:44389/api/search-employees-async/. Following are some sample search requests with different criteria:

Search for employees by first name or last name <br>
https://localhost:44389/api/search-employees-async/id=-1&firstName=John&lastName=''&updatedDateStart=0001-01-01&updatedDateEnd=0001-01-01&temperatureFrom=-1&temperatureTo=-1

Search for employees by record no <br>
https://localhost:44389/api/search-employees-async/id=1&firstName=''&lastName=''&updatedDateStart=0001-01-01&updatedDateEnd=0001-01-01&temperatureFrom=-1&temperatureTo=-1

Search for employees with specified date range <br>
https://localhost:44389/api/search-employees-async/id=-1&firstName=''&lastName=''&updatedDateStart=2022-03-22&updatedDateEnd=2022-03-24&temperatureFrom=-1&temperatureTo=-1

Search for employees with specified temperature range <br>
https://localhost:44389/api/search-employees-async/id=-1&firstName=''&lastName=''&updatedDateStart=0001-01-01&updatedDateEnd=0001-01-01&temperatureFrom=35&temperatureTo=40
