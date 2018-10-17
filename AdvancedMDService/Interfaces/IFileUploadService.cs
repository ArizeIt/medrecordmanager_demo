using System;
using AdvancedMDDomain;
using AdvancedMDDomain.DTOs.Requests;
using System.Threading.Tasks;
using AdvancedMDDomain.DTOs.Responses;

namespace AdvancedMDInterface
{
    public interface IFileUploadService 
    {
        Task<PpmUploadFileResponse> Upload(Uri apiUri, string userContext, PpmUploadFileRequest fileRequest);
   
    }
}
