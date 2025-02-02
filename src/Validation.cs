using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

public static class Validation
{
    public static bool ValidDates(string startDate, string endDate)
    {
        DateTime startDateTime = DateTime.ParseExact(startDate,"HH:mm d/MM/yyyy", new CultureInfo("en-US"));
        DateTime endDateTime = DateTime.ParseExact(endDate,"HH:mm d/MM/yyyy", new CultureInfo("en-US"));
        bool validDates = DateTime.Compare(startDateTime, endDateTime) < 0 ? true : false;
        return validDates;
    }

    public static bool DateIsFormatted(string date)
    {
        string pattern = @"^\d{2}:\d{2}\s\d{2}/\d{2}/\d{4}$";
        return Regex.IsMatch(date, pattern);
    }
}