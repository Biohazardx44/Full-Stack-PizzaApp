# Full-Stack PizzaApp

## Version: 0.2.2 BETA

### About Full-Stack PizzaApp:

**Full-Stack PizzaApp** was made using .NET7, PostgreSQL & Angular.  
WORK IN PROGRESS. About README will be released once all features have been added and the app is out of Beta testing!

### Key Features:

WORK IN PROGRESS. Key Features README will be released once all features have been added and the app is out of Beta testing!

### How to Run the App:

Follow these steps to run Full-Stack PizzaApp locally:

1. **Clone the Repository:** Start by cloning this repository to your local machine using the following command: `git clone https://github.com/Biohazardx44/Full-Stack-PizzaApp.git`
2. **Install Dependencies:** Ensure you have the necessary dependencies installed, including [[Visual Studio Code]](https://code.visualstudio.com/), [[Visual Studio]](https://visualstudio.microsoft.com/downloads/), [[PostgreSQL]](https://www.postgresql.org/), [[pgAdmin 4]](https://www.pgadmin.org/) and [[NodeJS]](https://nodejs.org/en)
3. **Start the Solution:** Navigate to the directory where you cloned the repository, then proceed to `server > PizzaApp` and open the solution in Visual Studio
4. **Change the ConnectionString:** Navigate to `PizzaApp > appsettings.json` and update the connection string to match your local database setup
5. **Open NuGet Package Console:** Navigate to `Tools > NuGet Package Manager > Package Manager Console`
6. **Setup Database:** Set `PizzaApp.DataAccess` as the default project in the console, then initialize the database using the `add-migration <DB Name>` and `update-database` commands
7. **Access the App BackEnd:** Click on the `PizzaApp` button or find the URL in `PizzaApp > Properties > launchSettings.json` and keep it running in the background
8. **Open the App:** Go back to the directory where you cloned the repository and right-click to open with Visual Studio Code
9. **Navigate to the Directory:** Open the console from `Terminal > New Terminal` and move into the project directory with `cd client`
10. **Install Dependencies:** Install the necessary dependencies in the console with `npm install`
11. **Access the App FrontEnd:** In the console, execute the `npm start` or `ng serve` commands, then click on the `http://localhost:4200/` link while ensuring the backend app is running in the background