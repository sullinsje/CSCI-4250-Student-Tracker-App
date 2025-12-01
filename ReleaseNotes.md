# CSCI-4250-Student-Tracker-App | Release Notes

### Version: v1.0.0
### Date: December 1st, 2025
### Status: Initial Release

## Introduction and Core Features

This is the initial production release of the Student Tracker Application, designed to help instructors see student location data

The following core features are available in this release:

* **Student Tracking:** Students can create attendance records by sharing their location, logging it to the database
* **Record Viewing:** Instructors can view attendance records of all registered students. Students can view their own records as well
* **Authentication:** Users can login/register and access the previously mentioned features based on their role
* **User Management:** Administrators can login and manage all users within the app

## System Requirements & Deployment

This version is released as a **Self-Contained Deployment** bundle specifically for Windows machines.

* **Platform:** Windows 10 or newer (x64)
* **Framework:** ASP.NET Core (Runtime bundled)
* **Database:** SQLite (Embedded, file-based)

**Deployment Note:**
The application data is stored in the `Data.db` file within the application folder. To begin tracking, please follow the steps in the [Deployment Guide](#https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/blob/main/DeploymentGuide.md), which involves running the `LaunchApp.bat` file and navigating to `http://localhost:5013`.


## Known Limitations

As an initial release, please be aware of the following known limitations:

* The authentication system is rather barebones, meaning it is not incredibly secure and real emails are not required to register
* Due to the JavaScript Geolocation API we used, location data can sometimes lack in precision. This appears to be due to rounding errors in coordinates
* This application's functionality extends to cover only Student Tracking and attendance history; complex functionality such as Chat systems and notifications do not yet exist

