using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RestService
{
    [CollectionDataContract(Name = "companies", Namespace = "")]
    public class Companies : List<Company>
    {
    }

    [DataContract(Name = "company", Namespace = "")]
    public class Company
    {
        [DataMember(Name = "id", Order = 1)]
        public long id;

        [DataMember(Name = "name", Order = 1)]
        public string name;


        public Company(SQLiteDataReader reader)
        {
            id = (long)reader["id"];
            name = (string)reader["name"];
        }
    }
}
