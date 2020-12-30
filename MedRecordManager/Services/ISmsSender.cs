using System.Threading.Tasks;

namespace MedRecordManager.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
