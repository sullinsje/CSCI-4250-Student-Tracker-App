# CSCI-4250-Student-Tracker-App

## Installation & Running

### Installing

* Download the ZIP from the latest release
* Extract the application files to location of choice

### Executing program

Within the Application folder either:
* Click on ```LaunchApp.bat``` to run the batch script
  * The application should open within the default browser over localhost
 
* Click on ```StudentTrackerApp.exe``` to run the executable
  * Navigate to the terminal window
  * Click on http://localhost:5013
  * The application should open within the default browser over localhost
 

## Application Operation

### Registering an Account

* Navigate to either the Student or Teacher login pages from the cards on the homepage or the navigation bar at the top
* There will be a link to register if you do not have an account to login; clicking it will navigate you to the register page
* Enter the necessary credentials; you will receive messages from the application if the credentials do not meet the requirements
* A successful registration will assign you the role based on your current page (i.e. /Auth/Student or /Auth/Teacher)
* In the case of application errors, try navigating back to http://localhost:5013

### Logging In

* Navigate to either the Student or Teacher login pages from the cards on the homepage or the navigation bar at the top
* Enter the necessary credentials; you will receive messages from the application if the credentials do not meet the requirements
* A successful login will redirect you to your role's dashbord (i.e. /Student/Dashboard or /Teacher/Dashboard)
* In the case of application errors, try navigating back to http://localhost:5013
