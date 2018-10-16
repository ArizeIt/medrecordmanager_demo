using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PVAMCommon
{
    public class HttpWebClient
    {

        /// <summary>
        /// Generic Http WebPost method return string
        /// </summary>
        /// <param name="server"></param>
        /// <param name="cookie"></param>
        /// <param name="postmsg"></param>
        /// <returns></returns>
        public  async Task<string> WebPostAsync(Uri server, string cookie, string postmsg)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server.ToString());
            request.CookieContainer = new CookieContainer();

            // Transfer the active cookies to the request. We are primarily interested
            // in getting the usercontext cookie (token).
            if (cookie != null && cookie != string.Empty)
            {
                //request.CookieContainer.SetCookies(server, cookie);
                request.CookieContainer.Add(new Cookie("token", cookie) { Domain = server.Host });
            }
            request.Method = "POST";
            request.ContentType = "text/xml";


            // Send the request
            XmlTextWriter writer = new XmlTextWriter(request.GetRequestStream(), Encoding.UTF8)
            {
                Namespaces = false
            };
            writer.WriteRaw(postmsg);
            writer.Flush();
            writer.Close();

            // Return the response
            var response = await request.GetResponseAsync();
            StreamReader reader = new StreamReader( response.GetResponseStream());

            return reader.ReadToEnd();
        }


        public string WebPost(Uri server, string cookie, string postmsg)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(server.ToString());
            request.CookieContainer = new CookieContainer();

            // Transfer the active cookies to the request. We are primarily interested
            // in getting the usercontext cookie (token).
            if (cookie != null && cookie != string.Empty)
            {
                //request.CookieContainer.SetCookies(server, cookie);
                request.CookieContainer.Add(new Cookie("token", cookie) { Domain = server.Host });
            }
            request.Method = "POST";
            request.ContentType = "text/xml";


            // Send the request
            XmlTextWriter writer = new XmlTextWriter(request.GetRequestStream(), Encoding.UTF8)
            {
                Namespaces = false
            };
            writer.WriteRaw(postmsg);
            writer.Flush();
            writer.Close();

            // Return the response
            var response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            return reader.ReadToEnd();
        }
    }
}
