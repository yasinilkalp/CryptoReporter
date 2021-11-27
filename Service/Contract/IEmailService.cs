using CryptoReporter.Model;
using System.Threading.Tasks;

namespace CryptoReporter.Service.Contract
{
    public interface IEmailService
    {
        Task<bool> Send(MessageContent messageContent);
    }
}
