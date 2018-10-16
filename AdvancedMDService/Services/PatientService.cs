using AdvancedMDInterface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using PVAMCommon;
using AdvancedMDDomain.DTOs.Responses;

using AdvancedMDDomain.Lookups;
using System.Xml.Linq;

namespace AdvancedMDService
{
    public class PatientService : IPatientService
    {


        public async Task<IPpmResponse> AddPatient(Uri apiUrl, string userContext, PpmAddPatientRequest addPatientRequest)
        {
            addPatientRequest.Msgtime = DateTime.Now.ToString();
            addPatientRequest.Action = RequestAction.AddPatient.Value;
            addPatientRequest.Class = ActionClass.ApiClass.Value;

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, addPatientRequest.Serialize());

            var xDoc = XElement.Parse(response);
            var results = xDoc.Descendants().FirstOrDefault(x => x.Name == "Results");
            if (results != null)
            {
                var success = results.Attribute("success");

                if (success.Value == "1")
                {
                    return response.Deserialize<PpmAddPatientResponse>();
                }
                else if (success.Value == "0")
                {
                    var error = response.Deserialize<PpmDupliAddPatientResponse>().Error;
                    if (error.Fault.Detail.Code.Any() && error.Fault.Detail.Description == "Duplicate SSN found")
                    {
                        if (addPatientRequest.RequestPatientlist.Patient.Ssn == "000-00-0000")
                        {
                            addPatientRequest.Force = "1";
                            await AddPatient(apiUrl, userContext,addPatientRequest);
                        }
                    }
                    if (error.Fault.Detail.Code.Any() && error.Fault.Detail.Description == "The patient you are adding already exists as a responsible party. Select that person in the Responsible Party Name field instead of SELF.")
                    {
                        var respartyResponse =  (PpmLookUpResPartyResponse)LookupRespartyByName(apiUrl, userContext, addPatientRequest.RequestPatientlist.Patient.Name).Result;
                        if (respartyResponse != null && respartyResponse.Results != null && respartyResponse.Results.Resppartylist.Respparty != null)
                        {
                            addPatientRequest.RequestPatientlist.Patient.Respparty = "";
                            addPatientRequest.Resppartylist = null;
                            await AddPatient(apiUrl, userContext, addPatientRequest);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            else
            {
                return null;
            }
            return response.Deserialize<PpmDupliAddPatientResponse>();
        }

        public async Task<IPpmResponse> UpdatePatient(Uri apiUrl, string userContext, PpmUpdatePatientRequest updatePatientRequest)
        {
            updatePatientRequest.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            updatePatientRequest.Action = RequestAction.UpdatePatient.Value;
            updatePatientRequest.Class = ActionClass.ApiClass.Value;
            var apiClient = new HttpWebClient();

            var response = await apiClient.WebPostAsync(apiUrl, userContext, updatePatientRequest.Serialize());
            var xDoc = XElement.Parse(response);
            var results = xDoc.Descendants().FirstOrDefault(x => x.Name == "Results");


            if (results != null && string.IsNullOrEmpty(results.Value))
            {
                return response.Deserialize<PpmUpdatePatientResponse>();
            }

            return null;
        }

        public async Task<IPpmResponse> UpdateRespParty(Uri apiUrl, string userContext, PpmUpdateRespPartyRequest updateRespPartyRequest)
        {
            updateRespPartyRequest.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            updateRespPartyRequest.Action = RequestAction.UpdateRespParty.Value;
            updateRespPartyRequest.Class = ActionClass.Demographics.Value;

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, updateRespPartyRequest.Serialize());
            return response.Deserialize<PpmUpdateRespPartyResponse>();
        }

        public async Task<IPpmResponse> AddResparty(Uri apiUrl, string userContext, PpmAddResPartyRequest addResPartyRequest)
        {

            addResPartyRequest.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            addResPartyRequest.Action = RequestAction.AddResparty.Value;
            addResPartyRequest.Class = ActionClass.Demographics.Value;
            addResPartyRequest.Respparty.Accttype = "4";
            addResPartyRequest.Respparty.Employstatus = "1";
            addResPartyRequest.Respparty.Sex = "U";
            addResPartyRequest.Respparty.Fincharge = "1";
            addResPartyRequest.Respparty.Billcycle = "28";
            addResPartyRequest.Respparty.Sendstmt = "1";
            addResPartyRequest.Respparty.Title = "Mr";
            addResPartyRequest.Familychanges = "";

            var apiClient = new HttpWebClient();

            var response = await apiClient.WebPostAsync(apiUrl, userContext, addResPartyRequest.Serialize());

            return response.Deserialize<PpmUpdateRespPartyResponse>();
        }


        public async Task<IPpmResponse> LookupPatientByName(Uri apiUrl, string userContext, string name)
        {
            var lookuprquest = new PpmLookUpPatientRequest
            {
                Action = RequestAction.LookUpPatient.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Name = name
            };

            var apiClient = new HttpWebClient();
            var response = await  apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());

            return response.Deserialize<PpmLookUpPatientResponse>();
        }


        public async Task<IPpmResponse> LookupPatientBySsn(Uri apiUrl, string userContext, string ssn)
        {
            var lookuprquest = new PpmLookUpPatientRequest
            {
                Action = RequestAction.LookUpPatient.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Ssn = ssn
            };

            var apiClient = new HttpWebClient();
            var response = await  apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());
            return response.Deserialize<PpmLookUpPatientResponse>();
        }

        public async Task<IPpmResponse> SavePatientNote(Uri apiUrl, string userContext, string amdPatientId, string profileId, string note)
        {
            var lookuprquest = new PpmSavePatientNoteRequest
            {
                Action = RequestAction.SavePatientNote.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.MasterFile.Value,
                Useclienttime = "1",
                Id = amdPatientId,
                Masterfile = new Masterfile()
                {
                    Case_note = note,
                    Notetypefid = "11",
                    Patientfid = amdPatientId,
                    Profilefid = profileId,
                    Uid = ""
                }
            };
            try
            {
                var apiClient = new HttpWebClient();
                var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());
                return response.Deserialize<PpmSavePatientNoteResponse>();
            }
            catch (Exception)
            {
                return null;
            }
                     
        }


        public async Task<IPpmResponse> LookUpPatientByChartNo(Uri apiUrl, string userContext, string chartNo)
        {
            var lookuprquest = new PpmLookUpPatientRequest
            {
                Action = RequestAction.LookUpPatient.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                ChartNo = chartNo
            };

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());

            return response.Deserialize<PpmLookUpPatientResponse>();
        }

        public async Task<IPpmResponse> LookupRespartyBySsn(Uri apiUrl, string userContext, string ssn)
        {
            var lookuprquest = new PpmLookUpResPartyRequest
            {
                Action = RequestAction.LookupResParty.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Ssn = ssn
            };

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());
            return response.Deserialize<PpmLookUpResPartyResponse>();
        }

        public async Task<IPpmResponse> LookupRespartyByName(Uri apiUrl, string userContext, string name)
        {
            var lookuprquest = new PpmLookUpResPartyRequest
            {
                Action = RequestAction.LookupResParty.Value,
                Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                Class = ActionClass.ApiClass.Value,
                Page = "1",
                Name = name
            };

            var apiClient = new HttpWebClient();
            var response = await apiClient.WebPostAsync(apiUrl, userContext, lookuprquest.Serialize());
            return response.Deserialize<PpmLookUpResPartyResponse>();
        }
    }
}
