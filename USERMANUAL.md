# CSCI-4250-Student-Tracker-App | User Manual

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

### Logging Out

* To logout, hover over your account's email in the top right of the screen
* Below it, a ```Logout``` button should appear. Click on it to navigate to the logout page
* From here, you will be prompted to ensure you want to logout
* To proceed, click the ```Yes, Logout``` button
* You will be redirected to the home page

## Student Operations

### Clocking In

* From the Student dashboard, click on the ```Clock In Now``` button on the Clock In Card
* To clock in, click the ```Clock In Now``` button on the Clock In Card
  * You will receive a notification in the browser asking you to enable location services
  * In order for tracking functionality to work, you must share your location
* You will receive a message showing your coordinates followed by a message noting a successful clock in
* If you receive errors, it is likely an issue with location sharing
  * Try refreshing an re-enabling location services

### Clocking Out

* From the Student dashboard, click on the ```Clock In Now``` button on the Clock In Card
* To clock out, click the ```Clock Out Now``` button on the Clock Out Card
  * You will receive a notification in the browser asking you to enable location services
  * In order for tracking functionality to work, you must share your location
* You will receive a message noting a successful clock out
* If you receive errors, it is likely an issue with location sharing
  * Try refreshing an re-enabling location services

### View Attendance History

* Click on the ```View Attendance History``` button from either the Clock In page or from the Attendance History card in the Dashboard
* You should see all of your attendance entries or an empty set if you do not have any entries

## Teacher Operations

### View Student List

* From the Teacher dashboard, click on the ```View Student List``` button on the Student List card
* You will be redirected to a list of all the students registered in the application

### View a Student's Details

* From the Student List, click on the ```View Attendance``` button within the ```Actions``` column next to the desired student
* You will be redirected to the Attendance History page of the student with a list of all their attendance entries
* From here, you can click the button containing coordinates under the ```Location (Lat, Long)``` column to redirect to an entry's coordinates on Google Maps

## Admin Operations

### Logging In

* The Admin Login screen is hidden from users
* To access it, navigate to either Student or Teacher login
* Modify the url: ```/Auth/Login/Admin``` instead of, e.g, ```/Auth/Login/Student```
* Login with the credentials
  * admin@a.com
  * Admin1!
* You will be redirected to the User Management dashboard

### Creating New User

* From the User Management dashboard, click the ```Create New User``` button within the Add New User card
* You will be redirected the Create New User page
* Fill out the form with necessary information; you will be prompted if the information is invalid
* Click the ```Create User``` button to submit the user creation form
* You will be redirected to the User Management dashboard, where you can see the newly created user

### Viewing User Details

* To view details of a specific user:
  * Click on the eye icon within the ```Actions``` column next to their entry
  * You will be redirected to their details page, displaying their account information
  * From here you can also access their Edit and Delete pages
 
### Editing User Details

* To edit details of a specific user:
  * Click on the pencil icon within the ```Actions``` column next to their entry
  * You will be redirected to their edit page
  * From here you can alter their Name and Role
  * When complete, click the ```Save Changes``` button
  * You will be redirected to the User Management dashboard
* You can also access the Edit page from within the [Details](#viewing-user-details) page

### Deleting User

* To delete a specific user:
  * Click on the trash icon within the ```Actions``` column next to their entry
  * You will be redirected to their delete page
  * From here you can delete the user. You will receive warnings that this action is permanent
  * To complete deletion, click the ```Confirm & Delete User``` button
  * You will be redirected to the User Management dashboard
* You can also access the Delete page from within the [Details](#viewing-user-details) page

