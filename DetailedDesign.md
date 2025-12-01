# CSCI-4250-Student-Tracker-App | Detailed Design Specification 

## Introduction and Context

### Purpose
The purpose of this document is to serve as the **Detailed Design Specification** for the system, focusing primarily on the **UML Class Design** and the **Entity-Relationship Data Model (ERD)** to guide implementation.

---

## Structural Design (UML)

### UML Class Diagram
<img width="1627" height="1105" alt="UML" src="https://github.com/user-attachments/assets/96b61ea3-8b17-4df1-abe2-cd34f019cd1b" />

### Key Class Explanation
* **`Student`:** Responsible for modelling student records in the database
* **`AttendanceRecord`:** Responsible for modelling attendance records in the database
* **`ApplicationUser`:** Responsible for modelling users from the database
* **`StudentRepository`:** Responsible for methods implementing CRUD functionality for Student models
* **`AttendanceRepository`:** Responsible for methods implementing create and read functionality for AttendanceRecord models
* **`IdentityUserRepository`:** Responsible for methods implementing CRUD functionality for Users and Roles

---

## Data Design (ERD)

### Entity-Relationship Diagram (ERD)

<img width="796" height="816" alt="Screenshot From 2025-11-30 21-05-10" src="https://github.com/user-attachments/assets/ad49cc22-206e-4129-a23b-7a35bf1710da" />


### Schema Details
* **`Students`:** PK (`Id`), FK to `AspNetUsers`, important fields (`Name`, `UserId`).
* **`AttendanceRecords`:** PK (`Id`), FK to `Students` (`StudentID`), important fields (`Date`, `ClockInLatitude`, `ClockInLongitude`, `ClockType`).
* **`AspNetUsers`:** PK (`Id`), important fields (`UserName`, `Name`, `Email`, `PasswordHash`).
* **`AspNetRoles`:** PK (`Id`), important fields (`Name`).
* **`AspNetUserRoles`:** Join table for `AspNetUsers` and `AspNetRoles`. 
* **Data Persistence:** Used **SQLite** via **EF Core**

