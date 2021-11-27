using System.Threading.Tasks;

namespace CryptoReporter.Service.Contract
{
    public interface ITemplateService<T>
    {
        Task<string> CreateMessageContent(string html, T data);
    }
}
