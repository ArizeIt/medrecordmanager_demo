

using AdvancedMDDomain;

namespace AdvancedMDDomain.DTOs.Responses
{
    public class ErrorResponse : IPpmResponse
    {
        public string ErrorCode { get; set; }
        public string Description { get; set; }
    }
}
