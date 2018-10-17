using System;
using System.Threading.Tasks;
using AdvancedMDDomain;
namespace AdvancedMDInterface
{
    public interface ILoginService 
    {
        Task<IPpmResponse> ProcessLogin(Uri apiUrl,int noCooki, string username, string password, string officecode, string appname, string cookie);
    }
}
