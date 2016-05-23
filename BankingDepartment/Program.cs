using System;
using System.Configuration;
using System.ServiceModel;
using System.Windows.Forms;

namespace BankingDepartment
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BankingDepartment instance = new BankingDepartment();
            ServiceHost host = new ServiceHost(instance);
            host.Open();
            Console.WriteLine("Host open");  

            String hostUrl = ConfigurationManager.AppSettings["HostUrl"];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BankingDepartmentForm(instance, hostUrl));

            host.Close();
            Console.WriteLine("Host closed");
        }
    }
}
