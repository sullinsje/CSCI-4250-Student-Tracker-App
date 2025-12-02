# CSCI-4250-Student-Tracker-App 

Student Tracker web application for instructors to receive up-to-date student location data that they share

## Description

### Background

This application was designed for ETSU's College of Nursing Program where students can travel offsite for work studies, internships, etc., but instructors needed a way to know where students were located. They desired an application that would permit students to share their location within a web browser, saving this location data into a form of persistent storage for future access. Instructors would then be able to access this persistent data to view student locations in an easy-to-use interface.

### Vision

The vision for this project was a web application using the .NET web framework, allowing users to access the application from any location. The Minimal Viable Product was an application that would allow students to clock in and clock out (starting and ending location sharing), allow teachers to view the student location logs, and an authentication system to permit user account based authorization.

### Result

We have an application that meets the defined MVP, in addition to an admin panel to manage all user accounts. Users can register their accounts, being assigned roles based on the register page that they are on. They can then sign into their accounts and perform actions based on their roles. 
* Students will be sent to their dashboard, where they can navigate to their Clock In or Attendance History pages. They can create new Clock In submissions or view previous ones within these pages.
* Teachers will be sent to their dashboard, where they can navigate to the class list. They can view all students registered into the system, and clicking on a student navigates to their details page. It is here where they can see the students entire location history; they can click on the coordinates to navigate to Google Maps where it shows the location.
* Admins will be sent to their user management dashboard, permitting the creation, updates, and deletion of user accounts. 

## Getting Started

### [Developer Manual](https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/tree/main/DEVELOPERS_MANUAL)

### [Deployment Guide](https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/blob/main/DeploymentGuide.md)

### [User Manual](https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/blob/main/USERMANUAL.md)

### [Authors](https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/blob/main/TeamMembership.md)

### [Version History](https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/blob/main/ReleaseNotes.md)



