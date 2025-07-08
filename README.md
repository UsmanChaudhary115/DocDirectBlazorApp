# 🩺 DocDirect - Doctor Directory & Appointment System





> **DocDirect** is a comprehensive platform for users to discover doctors, book appointments, and manage healthcare interactions. Built with **ASP.NET Core MVC** and being migrated to **Blazor WebAssembly**, it provides a modern UI/UX experience with a powerful backend engine.

---

## 🚀 Features

- 👨‍⚕️ Doctor Directory with Specialization Filter
- 📅 Appointment Booking & Management
- 🧾 View Appointment Details
- 🧑‍💼 Patient Dashboard
- 📬 Real-Time Notifications via SignalR (Future Scope)
- 📥 Admin Panel with CRUD for Doctors & Users (Limited)
- 🌐 Responsive Design (Desktop, Tablet, Mobile)
- 🧠 Smart Search with Name & Specialty
- 🔐 Secure Authentication & Authorization

---

## 🧱 Architecture Overview

- **Frontend**: Razor Views → Migrating to **Blazor WebAssembly**
- **Backend**: ASP.NET Core Web API
- **Database**: SQL Server + Entity Framework Core
- **Real-Time**: SignalR (for updates/notifications)
- **Authentication**: ASP.NET Identity

---

## 🏁 Getting Started

### ⚙️ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server (LocalDB or Express)
- Visual Studio or VS Code

### 🔧 Setup Instructions

```bash
# 1. Clone the repository
git clone https://github.com/UsmanChaudhary115/DocDirectBlazorApp.git

# 2. Navigate to project folder
cd DocDirectBlazorApp

# 3. Apply Migrations
cd DocDirectBlazorApp.Server
 dotnet ef database update

# 4. Run the project
dotnet run
```

Access the app at `https://localhost:*`

---

## 📂 Project Structure

```
DocDirect/
├── Client/             # Blazor WebAssembly Frontend (in-progress)
├── Server/             # ASP.NET Core Web API Backend
├── Shared/             # Shared DTOs and Models 
└── README.md
```

---

## 🔐 Authentication & Roles

- Patient: Can book appointments, manage their dashboard.
- Doctor: Can manage schedule (coming soon).
- Admin: Full access to system management.

---

## 📆 Future Development

| Feature                       | Status      |
| ----------------------------- | ----------- |
| ✅ Patient Booking             | Completed   |
| ✅ Admin Panel CRUD            | Completed   |
| ✅ Migrate Razor to Blazor    | Completed   |
| 🔄 Doctor Schedule Management | Planned     |
| 🔄 Email Notifications        | Planned     |
| 🔄 File Upload (Reports)      | Planned     |

---

## 🤝 Contributing

We welcome contributions from the community! 🛠️

1. Fork the repository
2. Create a feature branch: `git checkout -b feature-name`
3. Commit changes and push: `git push origin feature-name`
4. Create a pull request 🚀

---

## 👨‍💻 Author

**Usman Ali**\
🌐 [GitHub](https://github.com/UsmanChaudhary115)\
📧 [usmanalim015@gmail.com](mailto\:usmanalim015@gmail.com)

---

## ⭐ Show Your Support

If you like this project, don't forget to ⭐ it on [GitHub](https://github.com/UsmanChaudhary115/DocDirect) and share it with your friends!

---

