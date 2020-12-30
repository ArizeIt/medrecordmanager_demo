using PracticeVelocityDomain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CucmsService.Interfaces
{
    public interface ISourceService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task<string> GetXmlFromFileAsync(string filename);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<IList<string>> GetSourceFilesAsync(string filePath, string fileName);

        Task<IList<Log_Record>> GetPatientRecordsAsync(string filename);
    }
}
