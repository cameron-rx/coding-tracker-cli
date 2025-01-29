using System.Globalization;

public class CodingSession 
{
    public Int64 Id {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}

    public CodingSession(Int64 Id, string StartDate, string EndDate)
    {
        this.Id = Id;
        this.StartDate = DateTime.ParseExact(StartDate,"HH:mm d/MM/yyyy", new CultureInfo("en-US"));
        this.EndDate = DateTime.ParseExact(EndDate,"HH:mm d/MM/yyyy", new CultureInfo("en-US"));
    }

    public TimeSpan CalculateDuration()
    {
        return EndDate - StartDate;
    }

}