using System;
using System.ServiceModel;

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
            ServiceHost host = new ServiceHost(typeof(BankingDepartment));
            host.Open();
            Console.WriteLine("Banking Department Active. Press <Enter> to close.");
            Console.ReadLine();
            host.Close();
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }
    }
}
