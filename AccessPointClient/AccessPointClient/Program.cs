using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AccessPointClient
{
    public static class Http
    {
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            byte[] response = null;
            using (WebClient client = new WebClient())
            {
                response = client.UploadValues(uri, pairs);
            }
            return response;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Access Point Simulator***");

            SimulateAccess();

        }

        private static void SimulateAccess()
        {           
            Console.WriteLine("Enter User Id (this value will be readed from biometric scan)");
            var userId = Console.ReadLine();

            Console.WriteLine("Enter AccessPoint Device Id (this value will be readed from hardware)");
            var accessPointId = Console.ReadLine();

            var description = "Finger Print Readed";

            var url = "http://localhost:26042/api/gainaccess";
            var postData = "?userId=" + userId;
            postData += "&accessPointId=" + accessPointId;
            postData += "&description=" + description;            
            url += postData;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";            
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine(responseString);
            
            Console.ReadLine();
            SimulateAccess();
        }
    }
}