# CSCI-4250-Student-Tracker-App | Deployment Guide

## 1. Installation and Setup

This application uses a **Self-Contained Deployment** of ASP.NET Core with an embedded **SQLite** database. No external software (like the .NET Runtime or a database server) is required.

* **Download:** Download the latest application ZIP file from the release page.
* **Extract:** Extract the contents of the ZIP file to your preferred location on your Windows machine (e.g., `C:\StudentTracker\`).


## 2. Executing the Program and Launching the Browser

The application includes a helper batch file (`LaunchApp.bat`) that performs two steps: starts the application server and automatically opens the application URL in your default browser.

### Option A: One-Click Launch (Recommended)

1.  Navigate to the extracted application folder.
2.  **Double-click** on **`LaunchApp.bat`**.
    * *This will open a terminal window to run the server, and automatically open your default browser to the application page.*

### Option B: Manual Execution

If the batch file fails, you can run the server and open the browser manually:

1.  Navigate to the extracted application folder.
2.  **Double-click** on **`StudentTrackerApp.exe`**.
    * *A terminal window will open, showing that the server is running on port 5013.*
3.  Open your default browser and navigate to: **`http://localhost:5013`**


## 3. Shutting Down the Application

To stop the web application server, simply close the terminal window that was opened by the `.bat` file or the `.exe`.

* **Data Note:** All application data is stored locally in the **`Data.db`** file within the application folder.
