using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Util
    {

        public static string GetRequest(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
