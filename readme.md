# Destinex

![Angular](https://img.shields.io/badge/Angular-20-red?logo=angular\&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-8-blue?logo=dotnet\&logoColor=white)
![Tailwind](https://img.shields.io/badge/TailwindCSS-3.3-blue?logo=tailwind-css\&logoColor=white)
![DaisyUI](https://img.shields.io/badge/DaisyUI-stable-purple)
![WeatherAPI](https://img.shields.io/badge/WeatherAPI-Official-blue)

A full-stack **Angular 20 + .NET 8** application with **role-based authentication**, **user management**, and **weather search** with a 3-day forecast for target destination.
UI is responsive and modern with **Tailwind CSS** & **DaisyUI**, and state management uses Angular **signals**.

---

## 🌐 Features

* ✅ Role-based authentication & authorization (frontend + backend)
* ✅ Admin dashboard to view, update, and delete users
* ✅ Weather search by location with current + 3-day forecast
* ✅ Responsive UI using Tailwind CSS & DaisyUI
* ✅ Zoneless signals for reactive UI in Angular 20
* ✅ Clean architecture for backend (Controllers, Repositories, Services, DTOs)

---

## 🗂 Project Structure

### Backend (`api`)

```
api/
├─ Controllers/        # Web API controllers
├─ Data/               # DbContext, migrations, seeding
├─ Entities/           # EF entities
├─ DTOs/               # Data Transfer Objects
├─ Interfaces/         # Service & repository interfaces
├─ Repositories/       # Concrete repository implementations
├─ Extensions/         # Extension methods
├─ Services/           # Business logic
├─ Program.cs          # App startup
└─ appsettings.json    # Configuration
```

### Frontend (`frontend`)

```
frontend/
├─ src/
│  ├─ app/
│  │  ├─ core/         # Services (Http, Toast, Auth)
│  │  ├─ features/     # Components (Admin, Weather, etc.)
│  │  ├─ models/       # TS interfaces & enums
│  │  └─ app.component.ts/html/css
│  ├─ assets/
│  └─ styles.css       # Tailwind + DaisyUI configuration
├─ angular.json
├─ package.json
└─ tsconfig.json
```

---

## ⚙️ Prerequisites

* Node.js ≥ 24
* Angular CLI ≥ 20
* .NET 8 SDK
* SQL Server / PostgreSQL / other EF-supported DB
* Optional: Azure account (for EntraID integration)

---

## 🚀 Running Locally

### Backend

```bash
cd api
dotnet restore
dotnet ef database update
dotnet run
```

Backend runs at: `http://localhost:5001`

### Frontend

```bash
cd frontend
npm install
ng serve
```

Frontend runs at: `http://localhost:4200`

> ⚠ Ensure your Angular services point to the correct API URL.

---

## 🔐 Authentication & Authorization

* JWT-based auth for API calls
* Role-based access (Admin / User)
* Backend enforces `[Authorize(Roles = "...")]`
* **To-do:** Integrate **Azure EntraID** to replace custom JWT auth

---

## ✨ UI / UX

* Tailwind CSS & DaisyUI for responsive design
* Hoverable tables & toggle buttons for roles
* Angular 20 **signals** for reactive UI
* Clean, modern dashboard for admins

---

## 📌 Fancy Todos

### Backend

* [ ] Integrate **Location List API** with global countries API 🌍
* [ ] Soft delete for users instead of permanent deletion 🗑️

### Frontend

* [ ] Autocomplete location search using backend countries API 🔍
* [ ] Enhanced Admin Dashboard: badges for roles, mobile responsiveness 📱
* [ ] Real-time weather updates every 10 minutes ⏱️ for subscribed users

### Authentication & Security

* [ ] Integrate **Azure EntraID / Azure AD B2C** 🔐
* [ ] Automatic JWT refresh with interceptor 🔁

### Testing & Quality

* [ ] Unit tests for backend services 🧪
* [ ] Component tests for Angular UI 🧩
* [ ] CI/CD pipeline setup 🚀

### Optional Enhancements

* [ ] Multi-language support (i18n) 🌐
* [ ] Dark/light theme toggle 🌓
* [ ] Export users as CSV / Excel 📄

---

## 🛠 Tech Stack

| Frontend          | Backend                  |
| ----------------- | ------------------------ |
| Angular 20        | .NET 8                   |
| TypeScript        | C#                       |
| Tailwind CSS      | Entity Framework Core    |
| DaisyUI           | SQL / PostgreSQL         |
| Angular Signals   | JWT Auth                 |
| HTTP Interceptors | Role-based Authorization |

---

## 📄 Notes

* Enable **CORS** in backend for Angular localhost
* Configure **HTTPS** and secure tokens for production
* Project follows **Angular Style Guide** and **Clean Architecture** principles