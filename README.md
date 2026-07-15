# Task Management API

## Project Overview
Task Management API is RestFull web API developed ASP.Net Core 9.
It allows user to create, update, delete and retrieve data of a tasks.
In this application also implemented a background service 
which runs every one minute to update the status of the tasks,
and automatically updates as "Expires if the due date is overdue."

## Prerequisites

- .NET SDK 9.0
- PostgreSQL 17 
- Visual Studio 2022
- pgAdmin 4

## Steps to Run

1. Create Project in Visual Studio by choosing "ASP.NET CORE WEP API".
2. Create the database in PostgresSQL. 
3. Write a connection string in the appsetting.json.
    eg.
    "ConnectionStrings": {
    "WebApiDatabase": "Driver= driver name; Server=host name;Port=port number;Database=db_name;Username=postgres;Password=pwd"
  }
4.Create a Rquired folders for Models, DataLinkLayer, Bussiness Layer.
5.Write a connectivity logics  in DataLinkLayer, bussiness logics in BL and controller endpoints in the coltrollers,
6. Built the project.
7. Run the Application.
8. Test the Apis in Swagger.

## Database Setup

1. Install Install PostgreSQL 17
2. Create new database Task Managment.
3. Create table for task.
4. Connect it to the Visual studio using Connection String in appsettings.json.
5. Save and run the Application.

## Api Details

### Create Task
 *Method : POST*  
 *EndPoint : api/CreateTask* 
 *Request Body :
 {
  "title": "Task Title",
  "description": "Task Description",
  "due_date": "2026-08-12T10:00:00"
  
}
- status automatically created as "Pending"
- Created date also will be automatically created
- and id is serial so it will be incremented automatically* 


### Get Task
  *Method : GET*  
 *EndPoint : api/GetTasks/{status}* 
 
 - its a two in one api. if we pass status as "null" we can get all records,
   id we pass status we will get the perticular record

 ### Get Task By Id
  *Method : GET*  
 *EndPoint : api/GetTasks/{id}* 
 


### Update Task
  
  *Method : PUT*  
 *EndPoint : api/UpdatetTask* 
 *Request Body :
 {
  "id": 1,
  "title": "Updated Task",
  "description": "Updated Description",
  "due_date": "2026-08-15T10:00:00",
  "status": "Completed"
}
- Updated date also will be automatically created

### Delete Task

*Method : DELETE*  
*EndPoint :api/DeleteTask/{id}* 
 

### Task Expairy

   Scheduled task expairy using bacground services. If the due date is 
   past from the current date it will automatically update status as "Expired".
   Every one minute this condition will be checked.


## Assumptions

- PostgreSQL is used as the database for this implementation.
- ADO.NET is used for database operations instead of Entity Framework.
- Task status values are: **Pending**, **In Progress**, **Completed**, and **Expired**.
- Due Date cannot be earlier than the current date while creating or updating a task.
- The `created_dt` field is automatically set when a task is created.
- The `updated_dt` field is automatically updated whenever a task is modified.
- A background service runs every one minute to update the status of overdue tasks to **Expired**.
- Tasks with status **Completed** are not updated to **Expired** by the scheduled job.


