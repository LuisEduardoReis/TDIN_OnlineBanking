using System;
using System.Data.SQLite;

namespace RestService
{
    public class RestService : IRestService
    {
        static Users users = new Users();
        SQLiteConnection db_conn;

        public RestService() {
            db_conn = new SQLiteConnection("Data Source=RestServer.db;Version=3;");
            db_conn.Open();
        }

        //-------------- ORDERS -------------------//

        public Orders GetOrders()
        {
            Orders orders = new Orders();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders", db_conn);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order(reader));
            }
            return orders;
        }

        public void AddOrder(Order order)
        {

            order.create(db_conn);

        }

        public Order GetOrder(string id)
        {
           
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders WHERE id = @id", db_conn);
            command.Parameters.AddWithValue("@id", Int32.Parse(id));

            SQLiteDataReader reader = command.ExecuteReader();

            return new Order(reader);
        }

        //-------------- USERS -------------------//

        public Users GetUsers() {
            return users;            
        }

        public Orders GetUserOrders(string client_id, string order_by_date)
        {
            Orders orders = new Orders();

            string query = "SELECT * FROM Orders WHERE client = @client";

            //add order by
            if (Convert.ToBoolean(order_by_date)) query += " ORDER BY order_date DESC";

            SQLiteCommand command = new SQLiteCommand("SELECT * FROM Orders WHERE client = @client", db_conn);
            command.Parameters.AddWithValue("@client", Int32.Parse(client_id));

            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order(reader));
            }
            return orders;
        }


        public User GetUser(string id)



    }
}
