# 🇧🇬 BulgarianHistory

An ASP.NET Core MVC web application for exploring the history of Bulgaria through an interactive timeline, categorized events, cities, famous people, and historical eras. Includes an admin dashboard for content management.

---

## 🌍 Features

- 📜 Interactive SVG timeline of Bulgarian history (600–2000 AD)
- 🏛️ Full CRUD support for:
  - Events
  - Eras
  - Cities
  - Famous People
- 🧠 View details with facts, sources, and connections
- 👨‍💼 Admin panel with restricted access
- 🗃️ Entity relationships:
  - Events linked to Eras, Cities, and Famous People
- 🌐 UTF-8 support for Bulgarian language
- 🎨 Clean Bootstrap 5 UI with dark/light contrast

---

## 🧱 Technologies

- ASP.NET Core MVC (.NET 7 or 8)
- Entity Framework Core (SQL Server)
- Identity for Authentication
- Bootstrap 5
- jQuery Validation
- LINQ + ViewModels + Partial Views

---

## 🧰 Setup Instructions

### 📦 Prerequisites

- [.NET 7+ SDK](https://dotnet.microsoft.com/download)
- SQL Server or LocalDB
- Visual Studio 2022+ or VS Code

### 🚀 Run the Project

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-username/BulgarianHistory.git
   cd BulgarianHistory
   ```

2. **Configure the database:**

   - Go to `appsettings.json` and set your connection string.
   - Default assumes `LocalDB`.

3. **Apply migrations:**

   ```bash
   dotnet ef database update
   ```

4. **Run the app:**

   ```bash
   dotnet run
   ```

5. Navigate to:  
   [https://localhost:5001](https://localhost:5001)

---

## 🔐 Admin Access

- Use Identity to register a user
- Promote user to `"Admin"` role via database or Seed method
- Admins can:
  - Add/Edit/Delete content
  - Access `/Admin` dashboard

---

## 📂 Project Structure

```
BulgarianHistory/
│
├── Controllers/
│   ├── EventController.cs
│   ├── EraController.cs
│   ├── CityController.cs
│   └── AdminController.cs
│
├── Data/
│   └── Entities/
│       ├── Event.cs
│       ├── Era.cs
│       ├── City.cs
│       └── ...
│
├── ViewModel/
│   └── Event/
│       ├── EventDetailsViewModel.cs
│       └── EventFormViewModel.cs
│
├── Views/
│   ├── Shared/
│   ├── Timeline/
│   ├── Event/
│   ├── Era/
│   └── City/
│
├── wwwroot/
│   └── css / js / img
│
├── appsettings.json
└── Program.cs / Startup.cs
```

---

## 🗣️ Localization (Български)

- Entire UI and content support Cyrillic
- Database and pages encoded with `UTF-8`
- Input validation and UI messages in Bulgarian

---

## 📈 Future Improvements

- 🔍 Full-text search
- 🖼️ Upload images instead of URLs
- 🗓️ Filter timeline by century or theme
- 📱 Responsive timeline layout for mobile
- 📚 Public API for historical data

---

## 🧾 License

MIT License © [Your Name / Organization]

---

## 🤝 Contributing

Pull requests are welcome! Please fork the repo and submit a PR.
