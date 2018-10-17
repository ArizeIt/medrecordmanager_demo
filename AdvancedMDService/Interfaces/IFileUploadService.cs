using System;
using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using System.Threading.Tasks;

namespace AdvancedMDInterface
{
    public interface IFileUploadService 
    {
        Task<IPpmResponse> Upload(Uri apiUri, string userContext, PpmUploadFileRequest fileRequest);
   
    }
}
