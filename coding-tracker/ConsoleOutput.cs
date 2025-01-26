using Spectre.Console;

// Handles all console output 
public static class ConsoleInteraction
{
    public static void Welcome()
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

    }
}
