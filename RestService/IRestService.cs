using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RestService
{
    [ServiceContract]
    public interface IRestService {

        [WebGet(UriTemplate = "/users", ResponseFormat = WebMessageFormat.Json)]
        [Description("Gets all users stored so far.")]
        [OperationContract]
        Users GetUsers();

    }

    [CollectionDataContract(Name="users", Namespace="")]
    public class Users : List<User> {
    }

    [DataContract(Name="user", Namespace="")]
    public class User {
    }
}
