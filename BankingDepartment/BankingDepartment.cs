using System;
using System.ServiceModel;
using Models;

namespace BankingDepartment
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BankingDepartment : IBankingDepartment
    {
        [OperationBehavior(TransactionScopeRequired = true)]
        public void message(string message)
        {
            Console.WriteLine(message);
        }

        public delegate void OrderEvent(Order order);
        public OrderEvent NewOrder;

        [OperationBehavior(TransactionScopeRequired = true)]
        public void newOrder(Order order)
        {
            NewOrder(order);
        }
    }
}
