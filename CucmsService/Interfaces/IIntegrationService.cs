using AdvancedMDDomain.DTOs.Responses;
using CucmsCommon.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrgentCareData.Models;

namespace CucmsService.Interfaces
{
    public interface IIntegrationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceFilename"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task ProcessSourceFile(string sourceFilename, string userName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="officeKey"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<IList<ImportResult>> BatchImportAsync(BatchJob job, string officeKey, DateTime startDate, DateTime endDate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitId"></param>
        /// <returns></returns>
        Task<ImportResult> ImportVisitAsync(int visitId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="apiToken"></param>
        /// <param name="chartDoc"></param>
        /// <param name="amdPatientId"></param>
        /// <returns></returns>
        Task<ResponseFile> AddChartDocAsync(Uri apiUrl, string apiToken, ChartDocument chartDoc, string amdPatientId);

        /// <summary>
        /// /
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="apiToken"></param>
        /// <param name="patDoc"></param>
        /// <param name="amdPatientId"></param>
        /// <returns></returns>
        Task<ResponseFile> AddPatientDocAsync(Uri apiUrl, string apiToken, PatientDocument patDoc, string amdPatientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="token"></param>
        /// <param name="visitId"></param>
        /// <param name="officekey"></param>
        /// <param name="amdProviderId"></param>
        /// <param name="amdPatientId"></param>
        /// <param name="facilityId"></param>
        /// <param name="existingPateint"></param>
        /// <returns></returns>
        Task<string> AddVisit(Uri apiUrl, string token, int visitId, string officeKey, string amdProviderId, string amdPatientId, string facilityId, bool existingPateint);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitRecord"></param>
        /// <param name="amdPatientId"></param>
        /// <param name="amdVisitId"></param>
        /// <param name="amdFacilityId"></param>
        /// <param name="batchNumber"></param>
        /// <returns></returns>
        Task<bool> AddCharges(Visit visitRecord, string amdPatientId, string amdVisitId, string amdFacilityId, string batchNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitId"></param>
        /// <param name="officeKey"></param>
        /// <returns></returns>
        Task<string> UpdateGuarantor(int visitId, string officeKey);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visitRecord"></param>
        /// <param name="amdPatientId"></param>
        /// <param name="amdVisitId"></param>
        /// <param name="amdFacilityId"></param>
        /// <param name="providerId"></param>
        /// <param name="existingPantient"></param>
        /// <returns></returns>
        Task<bool> SaveCharges(Visit visitRecord, string amdPatientId, string amdVisitId, string amdFacilityId, string providerId, bool existingPantient);




    }
}
