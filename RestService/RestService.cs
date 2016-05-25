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

        public Order ExecuteOrder(string order_id, double value)
        {
            SQLiteCommand command = new SQLiteCommand(
                "SELECT Orders.* , Clients.name as 'ClientName', Clients.email as 'ClientEmail', Companies.name as 'CompanyName' "+
                "FROM(Orders "+
                    "LEFT JOIN Clients ON Orders.client = Clients.id "+
                    "LEFT JOIN Companies ON Orders.company = Companies.id "+
                ") WHERE Orders.id = 1", db_conn);
            command.Parameters.AddWithValue("@id", Int32.Parse(order_id));

            SQLiteDataReader reader = command.ExecuteReader();
            Order order = new Order(reader);

            order.executed = true;
            order.execution_date = DateTime.Now.ToString(new CultureInfo("en-GB"));
            order.share_value = value;
            order.total_value = order.quantity * order.share_value;

            order.update(db_conn);

           
            Util.SendMail((string) reader["ClientEmail"], "[TDIN] Order executed",
                    "Hello " +reader["ClientName"]+ "!\n\n"+
                    "Your order to " +(order.type==0 ? "buy" : "sell")+ " " + order.quantity + 
                    " \"" +reader["CompanyName"]+ "\" shares has been executed at " + order.execution_date);

            return order;
        }

        //-------------- USERS -------------------//

        public Clients GetClients() {
            Clients clients = new Clients();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Clients", db_conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read()) clients.Add(new Client(reader));            

            setResponseCode(System.Net.HttpStatusCode.OK);
           
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
            if (!reader.HasRows) {
                setResponseCode(System.Net.HttpStatusCode.NotFound);
                return null;
            }

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
