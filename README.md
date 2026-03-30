# TaskScheduler_Hangfire

ASP.NET Core Web API project demonstrating background job scheduling using **Hangfire** with **CQRS** pattern and **MediatR**.

## What This Project Does

- **Fire-and-Forget Job**: Sends a welcome email in the background after user registration — without blocking the API response.
- **Recurring Job**: Automatically cleans up orders older than 30 days, runs daily at 2:00 AM.

## Technologies Used

- ASP.NET Core 8
- Hangfire (background job scheduling)
- MediatR (CQRS pattern)
- Entity Framework Core (InMemory for dev)
- Clean Architecture

## Project Structure

```
TaskScheduler.Domain/        → Entities only, no external dependencies
TaskScheduler.Application/   → Commands, Handlers, Interfaces
TaskScheduler.Infrastructure/→ EF Core DbContext, EmailService implementation
TaskScheduler.API/           → Controllers, Hangfire setup, Program.cs
```

## Hangfire Job Types Used

| Type | Where Used | Description |
|------|-----------|-------------|
| Fire-and-Forget | User Registration | Send welcome email once, in background |
| Recurring | Order Cleanup | Delete old orders daily at 2 AM |

## How to Run

```bash
dotnet run --project TaskScheduler.API
```

Then open:
- Swagger UI: `http://localhost:5000/swagger`
- Hangfire Dashboard: `http://localhost:5000/hangfire`

## Testing the Fire-and-Forget Job

POST to `/api/users/register`:

```json
{
  "name": "Mariam",
  "email": "mariam@example.com"
}
```

The API responds immediately, and the welcome email job appears in the Hangfire Dashboard.
