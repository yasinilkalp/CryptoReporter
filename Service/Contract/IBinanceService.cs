using CryptoReporter.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CryptoReporter.Service.Contract
{
    public interface IBinanceService
    {
        Task<CryptoPrice> GetPriceAsync(string symbol);
        Task<IEnumerable<CryptoPrice>> GetPricesAsync();
    }
}
