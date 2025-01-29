using System.Configuration;
using System.Collections.Specialized;

string? path;
path = ConfigurationManager.AppSettings.Get("dbPath");

/*
Expected date format hh:mm dd/MM/yyyy
*/
if (path != null) 
{
    Database db = new Database(path);
    ConsoleInteraction.Welcome();
}
else 
{
    Console.WriteLine("Cannot find db connection string in config file");
}