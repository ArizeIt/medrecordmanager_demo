﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.DTOs.Responses;
using AdvancedMDDomain.Lookups;
using AdvancedMDInterface;
using PVAMCommon;

namespace AdvancedMDService
{
    public class InsuranceService :  IInsuranceService
    {
       
        public async Task<IPpmResponse> AddInsurance(Uri apiUri, string userContext, PpmAddInsuranceRequest request)
        {
            var apiClient = new HttpWebClient();

            request.Action = RequestAction.AddInsurance.Value;
            request.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            request.Class = ActionClass.Demographics.Value;
            var response = await  apiClient.WebPostAsync(apiUri, userContext, request.Serialize());
            try
            {
                return response.Deserialize<PpmAddInsuranceResponse>();
            }
            catch (Exception ex)
            {

                return new PpmAddInsuranceResponse {
                    Error = ex.Message,
                    Results = null,
                };
            }
            
        }
    }
}