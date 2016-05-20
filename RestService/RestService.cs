using Models;
using RestService.BankingDepartment;
using System;
using System.Data.SQLite;
using System.Globalization;
using System.ServiceModel.Web;


namespace RestService
{
    public class RestService : IRestService
    {

        SQLiteConnection db_conn;
        BankingDepartmentClient bankingDepartment;

        public RestService()
        {
            db_conn = new SQLiteConnection("Data Source=RestServer.db;Version=3;");
            db_conn.Open();

            bankingDepartment = new BankingDepartmentClient();
        }

        //-------------- ORDERS -------------------//

        public Orders GetOrders()
        {
            Orders orders = new Orders();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders", db_conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) orders.Add(new Order(reader));
          
            return orders;
        }

        public void AddOrder(Order order)
        {
            order.order_date = DateTime.Now.ToString(new CultureInfo("en-GB"));
            order.execution_date = "";
            order.share_value = 0;
            order.total_value = 0;
            order.create(db_conn);

            bankingDepartment.newOrder(order);
        }

        public Order GetOrder(string id)
        {           
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders WHERE id = @id", db_conn);
            command.Parameters.AddWithValue("@id", Int32.Parse(id));

            SQLiteDataReader reader = command.ExecuteReader();

            return new Order(reader);
        }

        public Orders GetNonExecutedOrders()
        {
            Orders orders = new Orders();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders WHERE executed=0 ORDER BY order_date DESC", db_conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) orders.Add(new Order(reader));
            return orders;
        }

        public Order ExecuteOrder(string order_id)
        {
            Order order = GetOrder(order_id);

            order.executed = true;
            order.execution_date = DateTime.Now.ToString(new CultureInfo("en-GB"));

            order.update(db_conn);
    
            return order;
        }

        //-------------- USERS -------------------//

        public Clients GetClients() {
            Clients clients = new Clients();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Clients", db_conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client(reader));
            }

            setResponseCode(System.Net.HttpStatusCode.Accepted);
           
            return clients;
        }

        public Orders GetClientOrders(string client_id, string order_by_date)
        {
            Orders orders = new Orders();

            string query = "SELECT * FROM Orders WHERE client = @client";

            //add order by
            if (Convert.ToBoolean(order_by_date)) query += " ORDER BY order_date DESC";

            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders WHERE client = @client", db_conn);
            command.Parameters.AddWithValue("@client", Int32.Parse(client_id));

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) orders.Add(new Order(reader));

            return orders;
        }


        public Client GetClient(string id)
        {
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Clients WHERE id = @id", db_conn);
            command.Parameters.AddWithValue("@id", Int32.Parse(id));

            SQLiteDataReader reader = command.ExecuteReader();

            return new Client(reader);
        }

        // ------------ COMPANIES ------------------

        public Companies GetCompanies()
        {
            Companies companies = new Companies();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Companies", db_conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) companies.Add(new Company(reader));
            

            return companies;
        }

        //---------------------------//

        public void setResponseCode(System.Net.HttpStatusCode code)
        {
            WebOperationContext ctx = WebOperationContext.Current;
            ctx.OutgoingResponse.StatusCode = code;
        }


    }
}
