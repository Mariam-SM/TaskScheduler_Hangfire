# 🚀 How to Run TaskScheduler_Hangfire

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

---

## Steps to Run

### 1. Open Terminal in the project root folder

```bash
cd TaskScheduler_Hangfire
```

### 2. Restore NuGet packages

```bash
dotnet restore
```

### 3. Run the API project

```bash
dotnet run --project TaskScheduler.API/TaskScheduler.API.csproj
```

### 4. Open your browser

| URL | Description |
|-----|-------------|
| `https://localhost:5001/swagger` | Swagger UI – test the API |
| `https://localhost:5001/hangfire` | Hangfire Dashboard – see all jobs |

---

## Test the Fire-and-Forget Job

In Swagger, call **POST /api/users/register** with:

```json
{
  "name": "Ahmed Ali",
  "email": "ahmed@example.com"
}
```

✅ You'll get an instant response  
✅ In the console you'll see: `[EmailService] ✅ Welcome email sent to Ahmed Ali at ahmed@example.com` (after ~2 seconds)  
✅ In the Hangfire Dashboard the job will show as **Succeeded**

---

## Test the Recurring Job

The **CleanOldOrders** job runs automatically every day at 2 AM.  
To trigger it manually from the Hangfire Dashboard:

1. Go to `https://localhost:5001/hangfire`
2. Click **Recurring Jobs**
3. Find `clean-old-orders`
4. Click **Trigger now**

---

## Project Structure

```
TaskScheduler_Hangfire/
├── TaskScheduler.Domain/           # Entities only – no dependencies
│   └── Entities/Order.cs
├── TaskScheduler.Application/      # Business logic + Interfaces
│   ├── Interfaces/IEmailService.cs
│   └── Commands/
│       ├── SendWelcomeEmail/
│       └── CleanOldOrders/
├── TaskScheduler.Infrastructure/   # DB + Email implementation
│   ├── Persistence/AppDbContext.cs
│   ├── Services/EmailService.cs
│   └── CleanOldOrdersHandler.cs
└── TaskScheduler.API/              # Entry point
    ├── Controllers/UsersController.cs
    ├── Jobs/RecurringJobs.cs
    └── Program.cs
```
