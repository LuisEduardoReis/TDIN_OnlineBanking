using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace BankingDepartment
{
    public class BankingDepartment : IBankingDepartment
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void message(string message)
        {
            Console.WriteLine(message);
        }

        public void newOrder(Order order)
        {
            Console.WriteLine("New Order!");
        }
    }
}
