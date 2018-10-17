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
        Task<IList<ImportResult>> BatchImportAsync(BatchJob job, ProgramConfig config, DateTime startDate, DateTime endDate);
        Task<ImportResult> ImportVisitAsync(int visitId);
    }
}
