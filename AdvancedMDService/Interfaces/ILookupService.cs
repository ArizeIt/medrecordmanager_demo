using AdvancedMDDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMDInterface
{
    public interface ILookupService 
    {
        Task<IPpmResponse> LookupProviderByName(Uri apiUrl, string userContext, string providerName);
        Task<IPpmResponse> LookupProviderByCode(Uri apiUrl, string userContext, string code);
        Task<IPpmResponse> LookupFinClassByName(Uri apiUrl, string userContext, string finclassName);
        Task<IPpmResponse> LookupFinClassById(Uri apiUrl, string userContext, string fineclassCode);
        Task<IPpmResponse> LookupAcctType(Uri apiUrl, string userContext, string code);

        Task<IPpmResponse> LookUpCarrier(Uri apiUrl, string userContext, string name, string match);
        Task<IPpmResponse> LookUpCarrierByCode(Uri apiUrl, string userContext, string code);
        Task<IPpmResponse> LookUpDiagCode(Uri apiUrl, string userContext, string code);

        Task<IPpmResponse> LookUpResPartyByName(Uri apiUrl, string userContext, string name);
        Task<IPpmResponse> LookUpResPartyBySsn(Uri apiUrl, string userContext, string ssn);
        Task<IPpmResponse> LookUpResPartyById(Uri apiUrl, string userContext, string id);

        Task<IPpmResponse> LookUpProcCode(Uri apiUrl, string userContext, string code);

        Task<IPpmResponse> LookUpDemographic(Uri apiUrl, string userContext, string patientId);
    }
}
