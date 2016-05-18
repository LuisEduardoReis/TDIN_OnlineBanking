using System;
using System.ServiceModel.Web;
using System.Data.SQLite;

class RestHost {
    static void Main() {
        WebServiceHost host = new WebServiceHost(typeof(RestService.RestService));
        host.Open();

        SQLiteConnection db_conn = new SQLiteConnection("Data Source=RestServer.db;Version=3;");
        db_conn.Open();

        SQLiteCommand command = new SQLiteCommand("SELECT * FROM Clients", db_conn);
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            Console.WriteLine(reader["id"] + " " + reader["name"] + " " + reader["email"]);
        }

        Console.WriteLine("Rest service running");
        Console.WriteLine("Press ENTER to stop the service");
        Console.ReadLine();
        host.Close();
    }
}
