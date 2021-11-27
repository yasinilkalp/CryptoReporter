using System.Threading.Tasks;

namespace CryptoReporter.Service.Contract
{
    public interface ICryptoReportService
    {
        Task<bool> SendReport(string symbol);
    }
}
