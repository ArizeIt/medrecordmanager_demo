using AdvancedMDDomain.DTOs.Responses;
using CucmsCommon.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrgentCareData.Models;

namespace CucmsService.Interfaces
{
    public interface IIntegrationService
    {
        Task ProcessSourceFile(string sourceFilename, string userName);
        Task<IList<ImportResult>> BatchImportAsync(BatchJob job, int officeKey, DateTime startDate, DateTime endDate);
        Task<ImportResult> ImportVisitAsync(int visitId);

        Task<ResponseFile> AddChartDocAsync(Uri apiUrl, string apiToken, ChartDocument chartDoc, string amdPatientId);
        Task<ResponseFile> AddPatientDocAsync(Uri apiUrl, string apiToken, PatientDocument patDoc, string amdPatientId);
    }
}
