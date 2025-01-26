using Dapper;
using Microsoft.Data.Sqlite;

public class Database
{
    private string ConnectionString = "Data Source=Sessions.db;";

    public Database()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            string sql = "CREATE TABLE IF NOT EXISTS coding_sessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, StartDate Text, EndDate Text)";
            connection.Execute(sql);
        }
    }

}