

using Microsoft.Data.Sqlite;

namespace SQLiteBackuper
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            string source = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Loggbok", "database.sqlite");

            string destination_path = Path.Combine(Path.GetTempPath(), "Loggbok");

            if (!Directory.Exists(destination_path))
            {
                Directory.CreateDirectory(destination_path);
            }
            string destination = Path.Combine(destination_path, "database.sqlite");
            
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

            Console.WriteLine("-------------------------");
            Console.WriteLine("Backup of");
            Console.WriteLine(source);
            Console.WriteLine("successfully created in");
            Console.WriteLine(destination);
            Console.WriteLine("-------------------------");
        }
    }
}
