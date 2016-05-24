using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Models;
using Newtonsoft.Json;

namespace Counter
{
    public partial class Form1 : Form
    {
        int current_user;
        string hostUrl;
        Dictionary<long, Company> companies;

        public Form1(string hostUrl)
        {
            InitializeComponent();
            this.hostUrl = hostUrl;
            current_user = -1;


            // Get companies
            string res;
            companies = new Dictionary<long, Company>();
            res = Util.GetRequest(hostUrl + "/companies");
            if (res != null)
                foreach (Company company in JsonConvert.DeserializeObject<List<Company>>(res))
                {
                    companies.Add(company.id, company);
                    cmbCompany.Items.Add(company);
                }

            // Get types
            OrderType buy = new OrderType(0, "Buy");
            OrderType sell = new OrderType(1, "Sell");
            cmbType.Items.Add(buy);
            cmbType.Items.Add(sell);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            try
            {
                current_user = Convert.ToInt32(txtUserId.Text);
            }
            catch (Exception)
            {
                lblUserIdError.Text = "Id must be an integer";
                return;
            }

            lblUserIdError.Text = "";
            if (current_user == -1) return;


            String client = Util.GetRequest(hostUrl + "/clients/"+ current_user);
            if (client == null) lblUserIdError.Text = "Client not found";

            else {
                lblUserIdError.Text = "";

                txtQuantity.Text = "";
                cmbCompany.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;


                //Get client's orders
                String orders = Util.GetRequest(hostUrl + "/clients/" + current_user+"/orders");
                if (orders != null)
                    foreach (Order order in JsonConvert.DeserializeObject<List<Order>>(orders))
                    {
                        String order_str = (order.type == 0 ? "Buy" : "Sell") + " - ";
                        order_str += (companies.ContainsKey(order.company) ? companies[order.company].name : order.company + "") + " - ";
                        order_str += order.quantity;
                        
                        lstOrders.Items.Add(new ListViewItem(order_str, 1));
                        
                    }
            }



        }
    }
}
