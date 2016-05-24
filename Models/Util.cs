using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public static string PostRequest(string url, string body)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                byte[] byteArray = Encoding.UTF8.GetBytes(body);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException e) {
                Console.WriteLine(e.ToString());
            }

            return null;
        }

        public static void SendMail(string to, string subject, string body) {

            var fromAddress = new MailAddress("tdinfeup1516a2v1@gmail.com", "TDIN_A2V1");
            var toAddress = new MailAddress(to, "Client");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, "t123456789t")
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
