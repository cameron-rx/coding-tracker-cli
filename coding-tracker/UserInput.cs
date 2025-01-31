using System.Text.RegularExpressions;
using Spectre.Console;

public static class UserInput 
{
    public static string DateInput(string message)
    {
        string regexPattern = @"^\d{2}:\d{2}\s\d{2}/\d{2}/\d{4}$";

        string date = "";
        bool validDate = false;
        while (!validDate)
        {
            date = AnsiConsole.Ask<string>(message);
            validDate = Regex.IsMatch(date,regexPattern);
            if (!validDate) {
                AnsiConsole.MarkupLine("[bold red]Answer not in valid format try again.[/]");
            }
        }

        return date;
    }
}