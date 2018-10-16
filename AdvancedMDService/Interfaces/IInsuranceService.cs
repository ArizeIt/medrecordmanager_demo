using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using System;
using System.Threading.Tasks;

namespace AdvancedMDInterface
{
    public interface IInsuranceService 
    {
        Task<IPpmResponse> AddInsurance(Uri apiUri, string userContext, PpmAddInsuranceRequest request);
    }
}
