using System.Text.RegularExpressions;
using Spectre.Console;

public static class UserInput 
{
    public static string DateInput(string message)
    {

        string date = "";
        bool validDate = false;

        do
        {
            date = AnsiConsole.Ask<string>(message);
            validDate = Validation.DateIsFormatted(date);
            if (!validDate) {
                AnsiConsole.MarkupLine("[bold red]ERROR[/] Answer not in valid format.[bold yellow] Try again.[/]");
            }
        } while(!validDate);

        return date;
    }
}