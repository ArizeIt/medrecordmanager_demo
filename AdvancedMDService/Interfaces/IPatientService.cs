using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using System;
using System.Threading.Tasks;

namespace AdvancedMDInterface
{
    public interface IPatientService
    {
        Task<IPpmResponse> AddPatient(Uri apiUrl, string userContext, PpmAddPatientRequest addPatientRequest);
        Task<IPpmResponse> UpdatePatient(Uri apiUrl, string userContext, PpmUpdatePatientRequest updatePatientRequest);
        Task<IPpmResponse> UpdateRespParty(Uri apiUrl, string userContext, PpmUpdateRespPartyRequest updateRespPartyRequest);

        Task<IPpmResponse> AddResparty(Uri apiUrl, string userContext, PpmAddResPartyRequest addResPartyRequest);

        Task<IPpmResponse> LookupPatientByName(Uri apiUrl, string userContext, string name);
        Task<IPpmResponse> LookUpPatientByChartNo(Uri apiUrl, string userContext, string chartNo);
        Task<IPpmResponse> LookupPatientBySsn(Uri apiUrl, string userContext, string ssn);
        Task<IPpmResponse> LookupRespartyBySsn(Uri apiUrl, string userContext, string ssn);
        Task<IPpmResponse> LookupRespartyByName(Uri apiUrl, string userContext, string name);
        Task<IPpmResponse> SavePatientNote(Uri apiUrl, string userContext, string amdPatientId, string profileId, string note);
    }
}
