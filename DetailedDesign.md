# CSCI-4250-Student-Tracker-App | Detailed Design Specification 

## Introduction and Context

### Purpose
The purpose of this document is to serve as the **Detailed Design Specification** for the system, focusing primarily on the **UML Class Design** and the **Entity-Relationship Data Model (ERD)** to guide implementation.

### Design Objectives
List the goals and objectives this specific design (the models) aims to achieve, linked back to the functional requirements.

* [ ] Objective 1: Describe the primary goal of the feature or system being modeled.
* [ ] Objective 2: Outline secondary objectives or constraints (e.g., must support CRUD for student entities).

---

## Structural Design (UML)

### UML Class Diagram

[Image of UML Class Diagram]

### Key Class Explanation
Briefly explain the role and primary responsibilities of the most critical classes shown in the diagram.
* **`Student`:** Role and primary responsibilities 
* **`AttendanceRecord`:** Role and primary responsibilities 

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

---

## Validation and Review

### Testing Strategy
Briefly describe the testing strategy focused on validating the models defined above.
* **Unit Testing:** Focus on verifying the correctness of logic within classes (e.g., methods in `ProgressTrackerService`).
* **Integration Testing:** Focus on validating the database interactions (CRUD operations) defined by the ERD.
