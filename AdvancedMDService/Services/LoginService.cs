using AdvancedMDInterface;
using System;
using System.Globalization;
using AdvancedMDDomain.DTOs;
using AdvancedMDDomain;
using PVAMCommon;
using System.Linq;
using System.Xml.Linq;
using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.DTOs.Responses;
using AdvancedMDDomain.Lookups;
using System.Threading.Tasks;

namespace AdvancedMDService
{
    public class LoginService : ILoginService
    {       
        public async Task<IPpmResponse> ProcessLogin(Uri apiUrl, int noCooki, string username, string password, string officecode, string appname, string cookie)
        {
            var apiClient = new HttpWebClient();

            var loginMsg = new PpmLoginRequest
            {
                Action = RequestAction.Login.Value, 
                Class = ActionClass.Login.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                NoCooki = noCooki,
                Username = username,
                Password = password,
                Officecode = officecode,
                Appname = appname
            };

            var redirectMessage = await apiClient.WebPostAsync(apiUrl, cookie, loginMsg.Serialize());

            if (!string.IsNullOrEmpty(redirectMessage))
            {
                var xDoc = XElement.Parse(redirectMessage);
                var results = xDoc.Descendants().FirstOrDefault(x => x.Name == "Results");
                var userContext = xDoc.Descendants().FirstOrDefault(x => x.Name == "usercontext");

                if (results != null && results.HasAttributes)
                {
                    if (results.Attribute("success").Value == "1" &&
                        userContext != null)
                    {
                        return redirectMessage.Deserialize<PpmLoginResponse>();
                    }

                    var code = xDoc.Descendants().FirstOrDefault(x => x.Name == "code");
                    if (code != null && code.Value == "-2147220476")
                    {
                        var redirectResponse = redirectMessage.Deserialize<PpmLoginResponse>();
                        var redirecturl = new Uri(redirectResponse.Results.Usercontext.Webserver + "/xmlrpc/processrequest.asp");
                        await ProcessLogin(redirecturl, 1, username, password, redirectResponse.Results.Usercontext.Officecode, appname, null);
                    }
                }
                return redirectMessage.Deserialize<PpmLoginBadResponse>();
            }
            return new PpmLoginResponse()
            {
                Results = null,
                Error = "Login Failed"
            };
        }
    }
}
