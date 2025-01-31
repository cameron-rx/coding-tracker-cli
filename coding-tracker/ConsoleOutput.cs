using System.Globalization;
using System.IO.Pipelines;
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
            TODO: Abstract away into method
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
            AnsiConsole.MarkupLine("\nt[bold red]ERROR[/] Start date/time does not come before end date/time");
        }

        Console.Read();
    }

    private void DeleteSessionScreen() 
    {
        // Get id user wants to delete
        // run delete crud operation
        // tell user if option was corret
        int id = AnsiConsole.Prompt(new TextPrompt<int>("Enter id of session you would like to delete: "));
        bool result = db.DeleteSession(id);
        if (result)
        {
            AnsiConsole.MarkupLine("[bold green]Success![/] Session deleted from history");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]ERROR[/] Could not find session with that id.");

        }
        Console.Read();
    }

    private void ViewSessionsScreen()
    {
        AnsiConsole.Clear();
        // Id start end duration
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Start");
        table.AddColumn("End");
        table.AddColumn("Duration");
        table.Title = new TableTitle("[bold]List of all coding sessions[/]");

        List<CodingSession> sessions = db.GetAllSessions();

        foreach (CodingSession session in sessions)
        {
            table.AddRow(session.Id.ToString(), session.StartDate.ToString(), session.EndDate.ToString(), session.CalculateDuration().ToString());
        }

        AnsiConsole.Write(table);
        Console.Read();
    }
}
