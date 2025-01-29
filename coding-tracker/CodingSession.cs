using System.Globalization;

public class CodingSession 
{
    public int Id {get; set;}
    public DateTime StartDate {get; set;}
    public DateTime EndDate {get; set;}

    public CodingSession(int id, string startDate, string endDate)
    {
        this.Id = id;
        this.StartDate = DateTime.ParseExact(startDate,"hh:mm dd/MM/yyyy", new CultureInfo("en-US"));
        this.EndDate = DateTime.ParseExact(endDate,"hh:mm dd/MM/yyyy", new CultureInfo("en-us"));
    }

    public TimeSpan CalculateDuration()
    {
        return EndDate - StartDate;
    }

}