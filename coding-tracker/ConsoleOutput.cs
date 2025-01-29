using Spectre.Console;

// Handles all console output 
public static class ConsoleInteraction
{
    public static void WelcomeScreen()
    {
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[bold black on white]Welcome to coding session tracker[/]")
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

    private static void AddSessionScreen() 
    {

    }

    private static void DeleteSessionScreen() 
    {

    }

    private static void ViewSessionsScreen()
    {

    }
}
