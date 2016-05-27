using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace BankingDepartment
{
    public partial class BankingDepartmentForm : Form
    {
        SQLiteConnection db_conn;

        string hostUrl;
        Dictionary<long, Company> companies;
        Dictionary<long, Client> clients;

        Dictionary<long, Order> orders;


        public BankingDepartmentForm(BankingDepartment instance, string hostUrl)
        {
            InitializeComponent();

            instance.NewOrder += this.NewOrder;
            this.hostUrl = hostUrl;

            string res;

            // Get companies
            companies = new Dictionary<long, Company>();
            res = Util.GetRequest(hostUrl + "/companies");
            if (res != null)
                foreach (Company company in JsonConvert.DeserializeObject<List<Company>>(res))
                    companies.Add(company.id, company);

            // Get clients
            clients = new Dictionary<long, Client>();
            res = Util.GetRequest(hostUrl + "/clients");
            if (res != null)
                foreach (Client client in JsonConvert.DeserializeObject<List<Client>>(res))
                    clients.Add(client.id, client);

            // DB
            db_conn = new SQLiteConnection("Data Source=BankingDepartment.db;Version=3;");

            // Load Orders
            orders = new Dictionary<long,Order>();
            db_conn.Open();
            SQLiteDataReader reader = new SQLiteCommand("SELECT * FROM Orders", db_conn).ExecuteReader();
            while (reader.Read())
            {
                Order order = new Order(reader);
                orders.Add(order.id, order);
            }
            db_conn.Close();

            RefreshView();
        }

        private void NewOrder(Order order)
        {
            orders.Add(order.id, order);
            order.createWithId(db_conn);
            Console.WriteLine("neworder");

            BeginInvoke((Action)(() => {
                RefreshView();
            }));
        }

        private void RefreshView()
        {

            Console.WriteLine("refresh");
            orderViewListBox.Items.Clear();

            foreach(Order order in orders.Values)
            {   
                String order_str = order.id + " - ";
                order_str += (clients.ContainsKey(order.client) ? clients[order.client].name : order.client + "") + " - ";
                order_str += (order.type == 0 ? "Buy" : "Sell") + " - ";
                order_str += (companies.ContainsKey(order.company) ? companies[order.company].name : order.company + "") + " - ";
                order_str += order.quantity;

                orderViewListBox.Items.Add(new DisplayOrder(order.id, order_str));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                int i = Int32.Parse(txtValue.Text);
            } catch (Exception) { return; }

            Console.WriteLine("Sending POST order/{id}/execute");
            Util.PostRequest(hostUrl + "/orders/" + ((DisplayOrder)orderViewListBox.SelectedItem).Id.ToString() + "/execute", 
                txtValue.Text);
            orders.Remove(((DisplayOrder)orderViewListBox.SelectedItem).Id);
            Order.delete(db_conn, ((DisplayOrder)orderViewListBox.SelectedItem).Id);
            RefreshView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RefreshView();
        }
    }


    class DisplayOrder{
        public long Id;
        public String Order_str;

        public DisplayOrder(long id, String order_str)
        {
            this.Id = id;
            this.Order_str = order_str;
        }

        public override string ToString()
        {
            return Order_str;
        }

     }
}
