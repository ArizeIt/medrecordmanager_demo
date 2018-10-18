using AdvancedMDInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.Lookups;
using System.Globalization;
using PVAMCommon;
using AdvancedMDDomain.DTOs.Responses;

namespace AdvancedMDService
{
    public class LookupService : ILookupService
    {
       
        public async Task<IPpmResponse> LookupProviderByName(Uri apiUrl, string userContext, string providerName)
        {
            var lookuprquest = new PpmLookUpProviderRequest
            {
                Action = RequestAction.LookupProfile.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Name = providerName
            };

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());

            try
            {
                return response.Deserialize<PpmLookUpProviderResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpProviderResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<PpmLookUpProviderResponse> LookupProviderByNameAsync(Uri apiUrl, string userContext, string providerName)
        {
            var lookuprquest = new PpmLookUpProviderRequest
            {
                Action = RequestAction.LookupProfile.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Name = providerName
            };

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());
            try
            {
                return response.Deserialize<PpmLookUpProviderResponse>();
            }
            catch 
            {
                return new PpmLookUpProviderResponse
                {
                    Error = response,
                    Results = null
                };
            }
        }

        public async Task<PpmLookUpProviderResponse> LookupProviderByCode(Uri apiUrl, string userContext, string code)
        {
            var lookuprquest = new PpmLookUpProviderRequest
            {
                Action = RequestAction.LookupProfile.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Code = code
            };

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());

            try
            {
                return response.Deserialize<PpmLookUpProviderResponse>();
            }
            catch 
            {
                return new PpmLookUpProviderResponse
                {
                    Error = response,
                    Results = null
                };
            }
        }



        public async Task<IPpmResponse> LookupFinClassById(Uri apiUrl, string userContext, string finclassCode)
        {
            var lookuprquest = new PpmLookUpFinClassRequest
            {
                Action = RequestAction.LookupFinClass.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Code = finclassCode
            };
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());

            try
            {
                return response.Deserialize<PpmLookUpFinClassResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpFinClassResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> LookupAcctType(Uri apiUrl, string userContext, string code)
        {
            var lookuprquest = new PpmLookUpAcctTypeRequest
            {
                Action = RequestAction.LookupAcctType.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Code = code
            };
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());

            try
            {
                return response.Deserialize<PpmLookUpAcctTypeResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpAcctTypeResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="match">0 fuzzy, 1 extact</param>
        /// <returns></returns>
        public async Task<IPpmResponse> LookUpCarrier(Uri apiUrl, string userContext, string name, string match)
        {
            var lookuprquest = new PpmLookUpCarrierRequest
            {
                Action = RequestAction.LookUpCarrier.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.LookUp.Value,
                Page = "1",
                Name = name,
                Exactmatch = match
            };
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());

            try
            {
                return response.Deserialize<PpmLookUpCarrierResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpCarrierResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }


        public async Task<IPpmResponse> LookUpCarrierByCode(Uri apiUrl, string userContext, string code)
        {
            var lookuprquest = new PpmLookUpCarrierRequest
            {
                Action = RequestAction.LookUpCarrier.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.LookUp.Value,
                Page = "1",
                Code = code
            };
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());
            try
            {
                return response.Deserialize<PpmLookUpCarrierResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpCarrierResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<IPpmResponse> LookUpDiagCode(Uri apiUrl, string userContext, string code)
        {
            var lookuprquest = new PpmLookUpDiagCodeRequest
            {
                Action = RequestAction.LookupDiagCode.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Code = code,
                Codeset = "10",
                Exactmatch = "-1"
            };
            var requeststring = lookuprquest.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requeststring);
            try
            {
                return response.Deserialize<PpmLookupDiagCodeResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookupDiagCodeResponse
                {
                    Error = ex.Message,
                   Results = null
                };
            }
        }



        public async Task<IPpmResponse> LookUpResPartyByName(Uri apiUrl, string userContext, string name)
        {
            var request = new PpmLookUpResPartyRequest
            {

                Action = RequestAction.LookupResParty.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.LookUp.Value,
                Page = "1",
                Name = name
            };

            var requeststring = request.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requeststring);

            try
            {
                return response.Deserialize<PpmLookUpResPartyResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpResPartyResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> LookUpResPartyBySsn(Uri apiUrl, string userContext, string ssn)
        {
            var request = new PpmLookUpResPartyRequest
            {

                Action = RequestAction.LookupResParty.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.LookUp.Value,
                Page = "1",
                Ssn = ssn
            };

            var requeststring = request.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requeststring);

            try
            {
                return response.Deserialize<PpmLookUpResPartyResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpResPartyResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> LookUpResPartyById(Uri apiUrl, string userContext, string id)
        {
            var request = new PpmLookUpResPartyRequest
            {

                Action = RequestAction.LookupResParty.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.LookUp.Value,
                Page = "1",
                Account = id
            };

            var requeststring = request.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requeststring);
            try
            {
                return response.Deserialize<PpmLookUpResPartyResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpResPartyResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> LookUpProcCode(Uri apiUrl, string userContext, string code)
        {
            var lookuprquest = new PpmLookUpProcCodeRequest
            {
                Action = RequestAction.LookUpProcCode.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Code = code,
            };
            var requeststring = lookuprquest.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requeststring);
            try
            {
                return response.Deserialize<PpmLookUpProcCodeResponse>();
            }
            catch (Exception ex)
            {
                return new PpmLookUpProcCodeResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public async Task<IPpmResponse> LookUpDemographic(Uri apiUrl, string userContext, string patientId)
        {
            var demographicRequest = new PpmGetDemographicRequest
            {
                Action = RequestAction.GetDemographic.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.Demographics.Value,
                Patientid = patientId
            };
            var requestString = demographicRequest.Serialize();
            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, requestString);
            try
            {
                return response.Deserialize<PpmGetDemographicResponse>();
            }
            catch (Exception ex)
            {
                return new PpmGetDemographicResponse
                {
                    Error = ex.Message,
                    Results = null
                };
            }
        }

        public Task<IPpmResponse> LookupFinClassByName(Uri apiUrl, string userContext, string finclassName)
        {
            throw new NotImplementedException();
        }
    }
}
