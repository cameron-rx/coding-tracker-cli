using System.Globalization;
using System.IO.Pipelines;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
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
            var title = new Markup("[bold]Coding Session Tracker[/]");
            var panel = new Panel(title);
            //var paddedTitle = new Padder(panel);

            AnsiConsole.Write(panel);


            var prompt = new SelectionPrompt<string>()
            .PageSize(10)
            .AddChoices(new[] {
                "Start Session",
                "Add Session",
                "View Sessions",
                "Delete Session",
                "Exit"
            });

            var choice = AnsiConsole.Prompt(prompt);

            switch (choice)
            {
                case "Start Session":
                    StartSessionScreen();
                    break;
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
                    AnsiConsole.Clear();
                    Environment.Exit(0);
                    break;
            }
        }
    }

    private void StartSessionScreen()
    {
        DateTime startTime;
        DateTime endTime;
        var start = UserInput.YesNoInput("Start a new session?", true);

        if (start)
        {
            startTime = DateTime.Now;
            AnsiConsole.MarkupLine($"[bold green]Session Started[/]");

            bool finished = false;
            do
            {
                finished = UserInput.YesNoInput("End current session?", false);
            } while (!finished);

            endTime = DateTime.Now;
            AnsiConsole.MarkupLine($"[bold red]Session Ended[/]");

            AnsiConsole.Markup($"Start Time: [bold yellow]{startTime}[/] ");
            AnsiConsole.Markup($"End Time: [bold yellow]{endTime}[/] ");
            AnsiConsole.MarkupLine($"Duration: [bold yellow]{endTime-startTime}[/]");
            
            var save = UserInput.YesNoInput("Would you like to save this session?", true);

            if (save)
            {
                bool result = db.AddSession(startTime.ToString("HH:mm d/MM/yyyy"), endTime.ToString("HH:mm d/MM/yyyy"));
                if (result)
                {
                    AnsiConsole.MarkupLine("[bold green]Success![/] Session saved.");
                }
                else 
                {
                    AnsiConsole.MarkupLine("[bold red]ERROR[/] Session could not be saved");
                }
            }

            Console.Read();
            
        }
    }
    private void AddSessionScreen()
    {
        AnsiConsole.MarkupLine("[bold]Date and time entries must be in format [yellow]'00:00 01/01/2000'[/][/]");


        bool validDates = false;
        string startDate = "";
        string endDate = "";

        while (!validDates)
        {
            startDate = UserInput.DateInput("Enter the time and date of when the session started");
            endDate = UserInput.DateInput("Enter the time and date of when the session finished");
            validDates = Validation.ValidDates(startDate, endDate);
            if (!validDates)
            {
                AnsiConsole.MarkupLine("[bold red]ERROR[/] Start date/time does not come before end date/time. [bold yellow]Try again.[/]");
            }
        }


        bool dbStatus = this.db.AddSession(startDate, endDate);

        if (dbStatus)
        {
            AnsiConsole.MarkupLine("[bold green]Success![/] Session added to database.");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Fail![/] Error adding session to database.");
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
        // Id start end duration
        var table = new Table();
        table.AddColumn("[bold]Id[/]");
        table.AddColumn("[bold]Start[/]");
        table.AddColumn("[bold]End[/]");
        table.AddColumn("[bold]Duration[/]");
        //table.Title = new TableTitle("[bold]List of all coding sessions[/]")
        ;

        List<CodingSession> sessions = db.GetAllSessions();

        foreach (CodingSession session in sessions)
        {
            table.AddRow(session.Id.ToString(), session.StartDate.ToString(), session.EndDate.ToString(), session.CalculateDuration().ToString());
        }

        AnsiConsole.Write(table);
        Console.Read();
    }
}
