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

namespace Counter
{
    public partial class Form1 : Form
    {
        int current_user;
        string hostUrl;


        public Form1(string hostUrl)
        {
            InitializeComponent();
            this.hostUrl = hostUrl;
            current_user = -1;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnChangeUser_Click(object sender, EventArgs e)
        {
            try
            {
                current_user = Int32.Parse(txtUserId.Text);
            }
            catch (Exception)
            {
                lblUserIdError.Text = "Id must be an integer";
                return;
            }

            lblUserIdError.Text = "";

            String clients = Util.GetRequest(hostUrl + "/clients/"+current_user);
            if (clients == null) lblUserIdError.Text = "Client not found";
            else {
                lblUserIdError.Text = "";

                txtQuantity.Text = "";
                cmbCompany.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;




            }



        }
    }
}
