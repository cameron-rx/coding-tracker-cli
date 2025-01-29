﻿using System.Configuration;
using System.Collections.Specialized;

string? path;
path = ConfigurationManager.AppSettings.Get("dbPath");

if (path != null) 
{
    Database db = new Database(path);
    ConsoleInteraction.WelcomeScreen();
}
else 
{
    Console.WriteLine("Cannot find db connection string in config file");
}