using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [CollectionDataContract(Name = "clients", Namespace = "")]
    public class Clients : List<Client>
    {
    }

    [DataContract(Name = "client", Namespace = "")]
    public class Client
    {
        [DataMember(Name = "id", Order = 1)]
        public long id;

        [DataMember(Name = "name", Order = 1)]
        public string name;

        [DataMember(Name = "email", Order = 1)]
        public string email;


        public Client(SQLiteDataReader reader)
        {
            id = (long)reader["id"];
            name = (string)reader["name"];
            email = (string)reader["email"];
        }


    }
}
