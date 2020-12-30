using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.DTOs.Responses;
using System;
using System.Threading.Tasks;

namespace AdvancedMDInterface
{
    public interface IVisitService
    {
        Task<PpmAddVisitResponse> AddVisit(Uri apiUrl, string userContext, string clinic, string patientId, string providerId, string columHead, string visitType, string date, string timeIn, string duration);

        Task<IPpmResponse> SaveCharges(Uri apiUrl, string userContext, PpmSaveChargesRequest saveCharges);

        Task<IPpmResponse> UpdateVisitCharges(Uri apiUrl, string userContext, PpmUpdateVisitWithNewCharngeRequest updateCharngeRequest);

        Task<IPpmResponse> GetFees(Uri apiUrl, string userContext, PpmGetFeesRequest getFeesRequest);

        Task<IPpmResponse> GetEpisodes(Uri apiUrl, string userContext, PpmGetEpisodesRequest getEpisodesRequest);

        Task<IPpmResponse> AddNewBatch(Uri apiUrl, string userContext, PpmAddNewBatchRequest request);

        Task<IPpmResponse> AddPayment(Uri apiUrl, string userContext, PpmAddPaymentRequest request);

        Task<IPpmResponse> AddVisitwNoAppt(Uri apiUrl, string userContext, string clinic, string patientId, string providerId, string date);
    }
}
