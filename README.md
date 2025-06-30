# WarungKu POS - Technical Test Submission

This is a simple Point of Sale (POS) web application built using ASP.NET Core MVC 8 as part of a technical test for PT Tri Mulya Gemilang.

The application is built using a **code-first approach**, following best practices in clean architecture. It covers essential POS features such as transaction entry and daily summary, with additional modules like product management and role-based user access.

---

## 🚀 Features

- POS Transaction Entry
- Daily Transaction Summary
- User Registration & Login
- Role-based Access (Admin / User)
- Dashboard Overview
- Product Management (CRUD)
- Category Management (CRUD)
- Server-side & Client-side Validation

---

## 🧰 Technologies Used

- ASP.NET Core MVC 8
- Entity Framework Core 8.0.12 (Code-First)
- SQL Server
- AutoMapper
- Generic Repository Pattern
- jQuery & jQuery DataTables
- Bootstrap

---

## ⚙️ How to Run This Project

1. **Clone the repository**
   ```bash
   git clone https://github.com/hendidwipurwanto/WarungKu-POS
 2.**  Set up your database connection**
In appsettings.json, update the connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=WarungKuDb;Trusted_Connection=True;"
}

03. **Apply the database migration**
Using Package Manager Console:
                      Update-Database

4.**Run the application**
5.**Register a new user, log in, and use the system.**


📄 Wiki Documentation: https://github.com/hendidwipurwanto/WarungKu-POS/wiki

⚠️ Notes
The application was tested functionally using general usage scenarios.

No formal unit tests or logging systems are included.

If specific testing scenarios are provided, I'm happy to adjust or extend the system accordingly.

🙏 Acknowledgement
This project was created as part of the technical recruitment process for PT Tri Mulya Gemilang.

👨‍💻 Author
Hendi Dwi Purwanto
hendidwipurwanto@gmail.com



