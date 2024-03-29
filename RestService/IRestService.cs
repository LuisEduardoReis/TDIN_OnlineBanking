﻿using Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RestService
{
    [ServiceContract]
    public interface IRestService {

        [WebGet(UriTemplate = "/clients", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets all clients stored so far.")]
        [OperationContract]
        Clients GetClients();

        [WebGet(UriTemplate = "/clients/{id}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets one client by id.")]
        [OperationContract]
        Client GetClient(string id);

        [WebGet(UriTemplate = "/orders", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets all orders stored so far.")]
        [OperationContract]
        Orders GetOrders();

        [WebGet(UriTemplate = "/orders/{id}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets one order by id.")]
        [OperationContract]
        Order GetOrder(string id);


        [WebInvoke(Method = "POST", UriTemplate = "/orders", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("Adds one order.")]
        [OperationContract]
        void AddOrder(Order order);

        [WebGet(UriTemplate = "/orders/non_executed", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets all non executed orders stored so far.")]
        [OperationContract]
        Orders GetNonExecutedOrders();

        [WebInvoke(Method = "POST", UriTemplate = "/orders/{order_id}/execute", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("Execute one order.")]
        [OperationContract]
        Order ExecuteOrder(string order_id, double value);

        [WebGet(UriTemplate = "/clients/{client_id}/orders/{order_by_date=true}", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets users' orders by user id.")]
        [OperationContract]
        Orders GetClientOrders(string client_id, string order_by_date);

        [WebGet(UriTemplate = "/companies", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets all valid companies.")]
        [OperationContract]
        Companies GetCompanies();

    }


    

}
