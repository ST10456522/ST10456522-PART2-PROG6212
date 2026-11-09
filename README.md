# RaceDay Management System - POE

## Project Overview
RaceDay is a full-stack web application developed using ASP.NET Core, C#, and Microsoft SQL Server. The system is designed to manage race events, allowing Participants to browse and enrol in races, and Organisers to create events, manage categories, view enrolments, and capture results.

This solution consists of **two projects** inside a single Visual Studio solution:
1. **`part2POE` (Web API):** Handles all database operations, authentication, and business logic.
2. **`RaceDayMVC` (MVC Web Application):** The front-end interface that users interact with. It communicates with the Part 2 API via `HttpClient` and uses Session management for authentication.

## Features (Part 3)

### Authentication and Role-Based Navigation
- **Registration & Login:** Both Participants and Organisers can register and log in.
- **Session Management:** The MVC application maintains the user's authenticated state and role (Participant or Organiser) across all pages.
- **Protected Routes:** Unauthenticated users cannot access dashboards or enrolment pages.
- **Role-Based Menus:** Distinct navigation experiences based on the logged-in user's role.

### Organiser Interface
- **Dashboard:** View a summary of all created events.
- **Event Management:** Create, edit, and delete events (including name, description, date, location, distance, and type).
- **Category Management:** Access a page to manage age/distance categories for each event.
- **Enrolments View:** View participants who have enrolled in specific events, including their selected category and status.
- **Results Entry:** Capture finishing times and positions for enrolled participants.

### Participant Interface
- **Home Page:** Browse all upcoming events with a built-in search/filter option.
- **Event Details:** View full event information and available categories.
- **Enrolment:** Select a category from a dropdown and enrol in the event immediately.
- **My Enrolments:** View a personal list of all events enrolled in, displaying a "Confirmed" status.
- **My Results:** View personal race history (finish times and positions).
- **Profile:** View and update personal information and upload a profile picture.

### Design & UI
- **RaceDay Branding:** Custom CSS with a sporting theme (Energetic Orange and Dark Asphalt).
- **Responsive:** Built using Bootstrap for mobile and desktop compatibility.
- **Status Colors:** Distinct visual indicators (e.g., Green for Confirmed enrolments).

## 🛠️ Technologies Used
- **C# / ASP.NET Core** – API and MVC development
- **Microsoft SQL Server (LocalDB)** – Database management
- **ADO.NET (System.Data.SqlClient)** – Database connectivity in the API
- **Swagger / Swashbuckle** – API documentation
- **Bootstrap 5** – Frontend styling
- **xUnit** – Unit testing
- **Git and GitHub** – Version control
- **GitHub Actions** – Continuous integration workflow

## 🗄️ Database
The project uses a Microsoft SQL Server database named `Raceday`.
Ensure the database and required tables exist in your SQL Server instance before running the application. 

**Example connection string (in the API):**
```text
Data Source=(localdb)\api_endpoint;Database=Raceday;
