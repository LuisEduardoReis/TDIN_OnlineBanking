using System;
using System.Data.SQLite;

namespace RestService
{
    public class Order
    {
        public long id;
        public long client;
        public long type;
        public String company;
        public DateTime order_date;
        public DateTime execution_date;
        public double share_value;
        public double total_value;

        public Order() {}

        public Order(SQLiteDataReader reader) {
            id = (long) reader["id"];
            client = (long)reader["client"];
            type = (long)reader["type"];
            company = (string) reader["company"];
            order_date = DateTime.Parse((string)reader["order_date"]);
            execution_date = DateTime.Parse((string)reader["execution_date"]);
            share_value = (double) reader["share_value"];
            total_value = (double) reader["total_value"];
        }

        public void create(SQLiteConnection conn) {
            SQLiteCommand command = new SQLiteCommand("INSERT INTO Orders(client,type,company,order_date,execution_date,share_value,total_value) " + 
                "VALUES(@client,@type,@company,@order_date,@execution_date,@share_value,@total_value)",conn);
            command.Parameters.AddWithValue("@client",client);
            command.Parameters.AddWithValue("@type",type);
            command.Parameters.AddWithValue("@company", company);
            command.Parameters.AddWithValue("@order_date", order_date);
            command.Parameters.AddWithValue("@execution_date", execution_date);
            command.Parameters.AddWithValue("@share_value", share_value);
            command.Parameters.AddWithValue("@total_value",total_value);

            command.ExecuteNonQuery();
        }
        public void update(SQLiteConnection conn) {
            SQLiteCommand command = new SQLiteCommand(
                "UPDATE Orders SET client=@client, type=@type, company=@company, order_date=@order_date, execution_date=@execution_date, share_value=@share_value, total_value=@total_value " +
                "WHERE id=@id", conn);
            command.Parameters.AddWithValue("@client", client);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@company", company);
            command.Parameters.AddWithValue("@order_date", order_date);
            command.Parameters.AddWithValue("@execution_date", execution_date);
            command.Parameters.AddWithValue("@share_value", share_value);
            command.Parameters.AddWithValue("@total_value", total_value);
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}
