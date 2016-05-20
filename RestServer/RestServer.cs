using System;
using System.ServiceModel.Web;
using System.Data.SQLite;
using System.ServiceModel.Description;
using RestServer;

class RestHost {
    static void Main() {
        WebServiceHost host = new WebServiceHost(typeof(RestService.RestService));
        foreach (ServiceEndpoint EP in host.Description.Endpoints)
            EP.Behaviors.Add(new BehaviorAttribute());
        host.Open();

        
        SQLiteConnection db_conn = new SQLiteConnection("Data Source=RestServer.db;Version=3;");
        db_conn.Open();

		
        Console.WriteLine("Rest service running");
        Console.WriteLine("Press ENTER to stop the service");
        Console.ReadLine();
        host.Close();
    }
}
