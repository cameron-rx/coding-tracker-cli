public class CodingSession 
{
    private int Id;
    private DateTime Start;
    private DateTime End;
    private TimeSpan Duration;

    public CodingSession(int id, DateTime start, DateTime end)
    {
        Id = id;
        Start = start;
        End = end;
        Duration = end - start;
    }

}