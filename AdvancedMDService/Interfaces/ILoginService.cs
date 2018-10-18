using System;
using System.Threading.Tasks;
using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Responses;

namespace AdvancedMDInterface
{
    public interface ILoginService 
    {
        Task<PpmLoginResponse> ProcessLogin(Uri apiUrl,int noCooki, string username, string password, string officecode, string appname, string cookie);
    }
}
