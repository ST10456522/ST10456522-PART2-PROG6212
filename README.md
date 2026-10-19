# part2POE
Raceday Management API

 Project Overview

Raceday Management API is a web-based API developed using ASP.NET Core, C#, and Microsoft SQL Server. The system is designed to manage race events and simplify the process of registering participants, organising events, managing race categories, and recording race results.

The API supports two types of users: Participants and Organisers. Participants can view available events and enrol in races, while organisers can manage events and access participant enrolment information.

This project also includes unit tests to verify important application functionalities and a GitHub Actions workflow for automated building and testing.Features

User Authentication: Register and log in as a participant or organiser.

Participant Management: Register, retrieve, update, and delete participant information.

Organiser Management: Manage organiser information.

Event Management: Create, view, update, and delete race events.

Category Management: Create and retrieve race categories.

Race Enrolment: Allow participants to enrol in events and retrieve enrolment information.

Results Management: Record and retrieve participant race results.

API Testing: Test API endpoints using Swagger.

Unit Testing: Verify selected application functionalities.

Automated Workflow: Use GitHub Actions to build the solution and run tests.

🛠️ Technologies Used

C# – Application programming language

ASP.NET Core Web API – API development framework

Microsoft SQL Server – Database management

ADO.NET (System.Data.SqlClient) – Database connectivity

Swagger / Swashbuckle – API documentation and endpoint testing

xUnit – Unit testing

Git and GitHub – Version control and source code hosting

GitHub Actions – Continuous integration workflow

 Database

The project uses a Microsoft SQL Server database named Raceday.

The database manages information relating to:

Participants

Organisers

Authentication

Events

Categories

Enrolments

Race results

The application uses a SQL Server LocalDB connection string during local development.

Example connection string:

Data Source=(localdb)\api_endpoint;Database=Raceday;

Make sure the database and required tables exist in your SQL Server instance before running the application. Update the connection string if your local database instance uses a different name.

 API Documentation and Testing

The API can be explored using Swagger when the application is running and Swagger is enabled.

Open the Swagger URL displayed by your application, usually in this format:

https://localhost:<port>/swagger



Swagger allows you to view available endpoints, provide request parameters, submit requests, and inspect responses.

Running Unit Tests

The solution includes test projects for checking selected application functionality.

To run all tests in the solution, execute:

dotnet test

To run the tests in Visual Studio, open Test → Test Explorer, then select Run All Tests.

Some tests may require a configured database and valid test data. Ensure the database connection and required records are available before running database-dependent tests.

 Continuous Integration with GitHub Actions

The repository includes a GitHub Actions workflow intended to automate the following tasks when changes are pushed to GitHub or a pull request is created:

Check out the repository.

Set up the .NET SDK.

Restore project dependencies.

Build the solution.

Run unit tests.

Workflow results can be viewed under the Actions tab in the GitHub repository.

Note: The workflow must complete successfully for the automated build and test process to be considered verified. Database-dependent tests may require additional configuration on the GitHub-hosted runner.
<img width="1543" height="540" alt="workflow" src="https://github.com/user-attachments/assets/938e7430-807b-45e3-bf1e-e2aec2776829" />

https://youtu.be/pFjsR91F9d0
