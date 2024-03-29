﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Runtime.Serialization;

namespace Models
{
    [CollectionDataContract(Name = "orders", Namespace = "")]
    public class Orders : List<Order>
    {
    }

    [DataContract(Name = "order", Namespace = "")]
    public class Order
    {
        [DataMember(Name = "id", Order = 1)]
        public long id;

        [DataMember(Name = "client", Order = 1)]
        public long client;

        [DataMember(Name = "type", Order = 1)]
        public long type;

        [DataMember(Name = "quantity", Order = 1)]
        public long quantity;

        [DataMember(Name = "company", Order = 1)]
        public long company;

        [DataMember(Name = "order_date", Order = 1)]
        public string order_date;

        [DataMember(Name = "execution_date", Order = 1)]
        public string execution_date;

        [DataMember(Name = "share_value", Order = 1)]
        public double share_value;

        [DataMember(Name = "total_value", Order = 1)]
        public double total_value;

        [DataMember(Name = "executed", Order = 1)]
        public bool executed;

        public Order() {}

        public Order(SQLiteDataReader reader) {
            id = (long) reader["id"];
            client = (long)reader["client"];
            type = (long)reader["type"];
            quantity = (long)reader["quantity"];
            company = (long) reader["company"];
            order_date = (string)reader["order_date"];
            execution_date = (string) reader["execution_date"];
            share_value = (double) reader["share_value"];
            total_value = (double) reader["total_value"];
            executed = ((long)reader["executed"]) > 0;
        }



        public void create(SQLiteConnection conn) {
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO Orders(client,type,quantity, company,order_date,execution_date,share_value,total_value) " + 
                "VALUES(@client,@type,@quantity, @company,@order_date,@execution_date,@share_value,@total_value)",conn);
            command.Parameters.AddWithValue("@client",client);
            command.Parameters.AddWithValue("@type",type);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.Parameters.AddWithValue("@company", company);
            command.Parameters.AddWithValue("@order_date", order_date);
            command.Parameters.AddWithValue("@execution_date", execution_date);
            command.Parameters.AddWithValue("@share_value", share_value);
            command.Parameters.AddWithValue("@total_value",total_value);

            command.ExecuteNonQuery();

            // Get id
            SQLiteCommand command_rowid = new SQLiteCommand("SELECT last_insert_rowid() as ROWID;", conn);
            SQLiteDataReader reader = command_rowid.ExecuteReader();
            this.id = (long) reader["ROWID"];
            conn.Close();
        }

        public void createWithId(SQLiteConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO Orders(id,client,type,quantity, company,order_date,execution_date,share_value,total_value) " +
                "VALUES(@id,@client,@type,@quantity, @company,@order_date,@execution_date,@share_value,@total_value)", conn);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@client", client);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.Parameters.AddWithValue("@company", company);
            command.Parameters.AddWithValue("@order_date", order_date);
            command.Parameters.AddWithValue("@execution_date", execution_date);
            command.Parameters.AddWithValue("@share_value", share_value);
            command.Parameters.AddWithValue("@total_value", total_value);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
        }

        public void update(SQLiteConnection conn) {
            if (conn.State != System.Data.ConnectionState.Open) if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SQLiteCommand command = new SQLiteCommand(
                "UPDATE Orders SET client=@client, type=@type, quantity=@quantity, company=@company, order_date=@order_date, execution_date=@execution_date, share_value=@share_value, total_value=@total_value, executed=@executed " +
                "WHERE id=@id", conn);
            command.Parameters.AddWithValue("@client", client);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.Parameters.AddWithValue("@company", company);
            command.Parameters.AddWithValue("@order_date", order_date);
            command.Parameters.AddWithValue("@execution_date", execution_date);
            command.Parameters.AddWithValue("@share_value", share_value);
            command.Parameters.AddWithValue("@total_value", total_value);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@executed", executed ? 1 : 0);

            command.ExecuteNonQuery();
            conn.Close();
        }

        public static void delete(SQLiteConnection conn, long id) {
            if (conn.State != System.Data.ConnectionState.Open) conn.Open();
            SQLiteCommand command = new SQLiteCommand("DELETE FROM Orders WHERE id=@id",conn);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
            conn.Close();
        }

       /*public override string ToString()
        {
            return client +"-"+ (type == 0 ? "Buy" : "Sell") + " - " + 
                    (companies.ContainsKey(order.company) ? companies[order.company].name : order.company + "") + " - ";
                    order_str += order.quantity;
        }*/
    }

   


}
