using AdvancedMDDomain.DTOs.Responses;
using System;
using System.Threading.Tasks;

namespace AdvancedMDInterface
{
    public interface ILoginService
    {
        Task<PpmLoginResponse> ProcessLogin(Uri apiUrl, int noCooki, string username, string password, string officecode, string appname, string cookie);
    }
}
