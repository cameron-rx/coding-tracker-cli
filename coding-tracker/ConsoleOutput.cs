using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Spectre.Console;

// Handles all console output 
public class ConsoleInteraction
{
    private Database db;
    public ConsoleInteraction(Database db)
    {
        this.db = db;
    }
    public void WelcomeScreen()
    {
        while (true)
        {
            AnsiConsole.Clear();
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[bold black on white]Coding Session Tracker[/]")
                .PageSize(10)
                .AddChoices(new[] {
            "Add Session", "View Sessions", "Delete Session",
            "Exit"
            }));

            switch (choice)
            {
                case "Add Session":
                    AddSessionScreen();
                    break;
                case "View Sessions":
                    ViewSessionsScreen();
                    break;
                case "Delete Session":
                    DeleteSessionScreen();
                    break;
                case "Exit":
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void AddSessionScreen() 
    {
        string regexPattern = @"^\d{2}:\d{2}\s\d{2}/\d{2}/\d{4}$";
        AnsiConsole.MarkupLine("[bold]Date and time entries must be in format [yellow]'00:00 01/01/2000'[/][/]");


        // TODO: Create method to reduce code repetition here
        string startDate = "";
        bool validStartDate = false;
        while (!validStartDate)
        {
            startDate = AnsiConsole.Ask<string>("Enter the time and date of when the session started: ");
            validStartDate = Regex.IsMatch(startDate,regexPattern);
            if (!validStartDate) {
                AnsiConsole.MarkupLine("[bold red]Answer not in valid format try again.[/]");
            }
        }

        string endDate = "";
        bool validEndDate = false;
        while (!validEndDate)
        {
            endDate = AnsiConsole.Ask<string>("Enter the time and date of when the session finished");
            validEndDate = Regex.IsMatch(endDate,regexPattern);
            if (!validEndDate) {
                AnsiConsole.MarkupLine("[bold red]Answer not in valid format try again.[/]");
            }
        }

        /*
            TODO: VALIDATION TO MAKE SURE END DATE IS AFTER START DATE
        */

        DateTime startDateTime = DateTime.ParseExact(startDate,"HH:mm d/MM/yyyy", new CultureInfo("en-US"));
        DateTime endDateTime = DateTime.ParseExact(endDate,"HH:mm d/MM/yyyy", new CultureInfo("en-US"));
        bool validDates = DateTime.Compare(startDateTime, endDateTime) < 0 ? true : false;

        if (validDates)
        {

            bool dbStatus = this.db.AddSession(startDate, endDate);

            if (dbStatus)
            {
                AnsiConsole.MarkupLine("\n[bold green]Success![/] Session added to database.");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold red]Fail![/] Error adding session to database.");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[bold red]ERROR[/] Start date/time does not come before end date/time");
        }

        Console.Read();
    }

    private void DeleteSessionScreen() 
    {

    }

    private void ViewSessionsScreen()
    {

    }
}
