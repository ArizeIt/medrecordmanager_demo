using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.DTOs.Responses;
using AdvancedMDInterface;
using PVAMCommon;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using AdvancedMDDomain.Lookups;
using System.Threading.Tasks;

namespace AdvancedMDService
{
    public class VisitService : IVisitService
    {
      
        public async Task<PpmAddVisitResponse> AddVisit(Uri apiUrl, string userContext, string clinic, string patientId, string providerId, string columnHead, string visitType, string date, string timeIn, string duration)
        {
            

            var addVisitRequest = new PpmAddVisitRequest()
            {
                Action = RequestAction.AddVisit.Value,
                Class = ActionClass.ChangeEntry.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
            };
            if (columnHead != "0")
            {
                addVisitRequest.Appt = new Appt()
                {
                    Date = date,
                    PatientId = patientId,
                    ProfileId = providerId,
                    Types = visitType,
                    Time = timeIn,
                    Duration = duration,
                    Column = columnHead,
                    Force = "1",
                };
            }
            else
            {
                addVisitRequest.Appt = new Appt()
                {
                    Date = date,
                    PatientId = patientId,
                    ProfileId = providerId,
                    Force = "1",
                };
            }
            try
            {
                var apiClient = new HttpWebClient();
                var response = await apiClient.WebPostAsync(apiUrl, userContext, addVisitRequest.Serialize());            
                return  response.Deserialize<PpmAddVisitResponse>();
            }
            catch (Exception ex)
            {
                return new PpmAddVisitResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> AddVisitwNoAppt(Uri apiUrl, string userContext, string clinic, string patientId, string providerId,  string date)
        {


            var addVisitRequest = new PpmAddVisitRequest()
            {
                Action = RequestAction.AddVisit.Value,
                Class = ActionClass.ChangeEntry.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Appt = new Appt()
                {
                    Date = date,
                    PatientId = patientId,
                    ProfileId = providerId,
                    Force = "1",
                }

            };
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, addVisitRequest.Serialize());
            try
            {
                var responseMsg = response.Deserialize<PpmAddVisitResponse>();
                //if(responseMsg.Error.Contains)
                return response.Deserialize<PpmAddVisitResponse>();
            }
            catch (Exception ex)
            {
                return new PpmAddVisitResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> SaveCharges(Uri apiUrl, string userContext, PpmSaveChargesRequest request)
        {

            request.Action = RequestAction.SaveCharges.Value;
            request.Class = ActionClass.ApiClass.Value;
            request.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            var requestString = request.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requestString);
            try
            {
                return response.Deserialize<PpmSaveChargesResponse>();
            }
            catch (Exception ex)
            {
                return new PpmSaveChargesResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> UpdateVisitCharges(Uri apiUrl, string userContext, PpmUpdateVisitWithNewCharngeRequest updateCharngeRequest)
        {
            updateCharngeRequest.Action = RequestAction.UpdateVisitWithCharges.Value;
            updateCharngeRequest.Class = ActionClass.ChangeEntry.Value;
            updateCharngeRequest.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            var requestString = updateCharngeRequest.Serialize();

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requestString);
            try
            {
                return response.Deserialize<PpmSaveChargesResponse>();
            }
            catch (Exception ex)
            {
                return new PpmSaveChargesResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> GetFees(Uri apiUrl, string userContext, PpmGetFeesRequest getFeesRequest)
        {
            getFeesRequest.Action = RequestAction.GetFees.Value;
            getFeesRequest.Class = ActionClass.ChangeEntry.Value;
            getFeesRequest.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            //hard code this value to get fee..
            getFeesRequest.Chargeschedid = "feesch21092";

            var requestString = getFeesRequest.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requestString);
            try
            {
               return response.Deserialize<PpmGetFeesResponse>();

            }
            catch (Exception ex)
            {
                return new PpmGetFeesResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> GetEpisodes(Uri apiUrl, string userContext, PpmGetEpisodesRequest getEpisodesRequest)
        {

            getEpisodesRequest.Action = RequestAction.GetEpisodes.Value;
            getEpisodesRequest.Class = ActionClass.Demographics.Value;
            getEpisodesRequest.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            
            var requestString = getEpisodesRequest.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requestString);
            try
            {
                return response.Deserialize<PpmGetEpisodesResponse>();

            }
            catch (Exception ex)
            {
                return new PpmGetEpisodesResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> AddNewBatch(Uri apiUrl, string userContext, PpmAddNewBatchRequest request)
        {
            request.Action = RequestAction.NewBatch.Value;
            request.Class = ActionClass.Batches.Value;
            request.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, request.Serialize());
            try
            {
                return response.Deserialize<PpmNewBatchResponse>();

            }
            catch (Exception ex)
            {
                return new PpmNewBatchResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> AddPayment(Uri apiUrl, string userContext, PpmAddPaymentRequest request )
        {
            request.Action = RequestAction.AddPayment.Value;
            request.Class = ActionClass.Payement.Value;
            request.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, request.Serialize());
            try
            {
                return response.Deserialize<PpmAddPaymentResponse>();

            }
            catch (Exception ex )
            {
                return new PpmAddPaymentResponse {
                    Error = ex.Message,
                    Results = null
                };
            }
        }
    }
}
