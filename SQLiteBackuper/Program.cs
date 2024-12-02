

using Microsoft.Data.Sqlite;

namespace SQLiteBackuper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string source = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Loggbok", "database.sqlite");
            string destination = Path.Combine(Path.Combine(Path.GetTempPath(), "Loggbok"), "database.sqlite");
            
            string source_connect_string = $@"Data Source={source};";
            string destination_connect_string = $@"Data Source={destination};";
            
            using SqliteConnection sqlite_conn1 = new(source_connect_string);
            using SqliteConnection sqlite_conn2 = new(destination_connect_string);

            sqlite_conn1.Open();
            sqlite_conn2.Open();
            
            sqlite_conn1.BackupDatabase(sqlite_conn2, "main", "main");
            
            sqlite_conn1.Close();
            sqlite_conn2.Close();
            
            SqliteConnection.ClearPool(sqlite_conn2);
        }
    }
}
