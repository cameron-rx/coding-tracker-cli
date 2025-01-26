using Spectre.Console;

public static class ConsoleOutput
{
    public static void Welcome()
    {
        AnsiConsole.Markup("[bold blue on white]Hello[/] [bold green]World[/][bold]![/]");
    }
}