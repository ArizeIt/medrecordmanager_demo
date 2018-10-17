using System;
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
    public class FileUploadService : IFileUploadService
    {
      

        public async Task<PpmUploadFileResponse> Upload(Uri apiUri, string userContext, PpmUploadFileRequest fileRequest)
        {
            var webclient = new HttpWebClient();
            fileRequest.Action = RequestAction.UploadFile.Value;
            fileRequest.Msgtime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            fileRequest.Class = ActionClass.Files.Value;
            var response = await webclient.WebPostAsync(apiUri, userContext, fileRequest.Serialize());
            try
            {
                return response.Deserialize<PpmUploadFileResponse>();
            }
            catch (Exception ex)
            {
                return new PpmUploadFileResponse
                {
                    Error = "Upload Failed: " + ex.InnerException.Message
                };
            }

        }      
    }
}

