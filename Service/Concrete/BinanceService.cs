using AutoMapper;
using Binance.Net;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoReporter.Model;
using CryptoReporter.Service.Contract;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoReporter.Service.Concrete
{
    public class BinanceService : IBinanceService
    {
        private readonly BinanceApiSettings _settings;
        private readonly IMapper _mapper;
        public BinanceService(IOptions<BinanceApiSettings> settings, IMapper mapper)
        {
            _settings = settings.Value;
            _mapper = mapper;
        }

        public async Task<CryptoPrice> GetPriceAsync(string symbol)
        {
            var client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(_settings.ApiKey, _settings.ApiSecret)
            });

            var price = await client.Spot.Market.GetPriceAsync(symbol);

            var mapped = _mapper.Map<CryptoPrice>(price.Data);

            return mapped;
        }

        public async Task<IEnumerable<CryptoPrice>> GetPricesAsync()
        {
            var client = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(_settings.ApiKey, _settings.ApiSecret)
            });

            var prices = await client.Spot.Market.GetPricesAsync();

            var mapped = _mapper.Map<IEnumerable<CryptoPrice>>(prices.Data);

            return mapped;
        }
    }
}
