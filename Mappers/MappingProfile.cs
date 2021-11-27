using AutoMapper;
using Binance.Net.Objects.Spot.MarketData;
using CryptoReporter.Model;

namespace CryptoReporter.Mappers
{

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BinancePrice, CryptoPrice>()
                .ReverseMap();
        }
    }
}
