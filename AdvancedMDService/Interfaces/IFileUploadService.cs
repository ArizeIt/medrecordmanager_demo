using AdvancedMDDomain.DTOs.Requests;
using AdvancedMDDomain.DTOs.Responses;
using System;
using System.Threading.Tasks;

namespace AdvancedMDInterface
{
    public interface IFileUploadService
    {
        Task<PpmUploadFileResponse> Upload(Uri apiUri, string userContext, PpmUploadFileRequest fileRequest);

    }
}
