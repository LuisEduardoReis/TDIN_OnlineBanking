using System;
using System.ServiceModel.Web;
using System.Data.SQLite;
using RestService;

class RestHost {
    static void Main() {
        WebServiceHost host = new WebServiceHost(typeof(RestService.RestService));
        host.Open();

        SQLiteConnection db_conn = new SQLiteConnection("Data Source=RestServer.db;Version=3;");
        db_conn.Open();

        SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders", db_conn);
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read()) {
            Order order = new Order(reader);
            order.company = "Test";
            order.update(db_conn);
        }


        Console.WriteLine("Rest service running");
        Console.WriteLine("Press ENTER to stop the service");
        Console.ReadLine();
        host.Close();
    }
}
