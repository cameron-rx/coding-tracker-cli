using System.ComponentModel;
using System.Data.SQLite;
using Dapper;
using Microsoft.Data.Sqlite;
public class Database
{
    private string ConnectionString;

    public Database(string path)
    {
        this.ConnectionString = path;
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            string sql = "CREATE TABLE IF NOT EXISTS coding_sessions (Id INTEGER PRIMARY KEY AUTOINCREMENT, StartDate Text, EndDate Text)";
            connection.Execute(sql);
        }
    }

    public bool AddSession(string startTime, string endTime)
    {
        // @value in sql statements allows for parameterised queries to prevent sql attacks
        string sql = "INSERT INTO coding_sessions (StartDate, EndDate) VALUES (@startDate, @endDate)";
        using (var connection = new SqliteConnection(ConnectionString))
        {
            var response = connection.Execute(sql,new {startDate = startTime, endDate = endTime});
            if (response > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }

    public bool DeleteSession(int id)
    {
        string sql = "DELETE FROM coding_sessions WHERE Id=@Id";

        using (var connection = new SqliteConnection(ConnectionString))
        {
            var response = connection.Execute(sql,new {Id = id});

            if (response > 0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }

    public List<CodingSession> GetAllSessions()
    {
        List<CodingSession> sessions;
        string sql = "SELECT * FROM coding_sessions";

        using (var connection = new SQLiteConnection(ConnectionString))
        {
            sessions = connection.Query<CodingSession>(sql).ToList();
        }

        return sessions;
    }

}