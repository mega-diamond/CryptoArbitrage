using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoArbitrageLibrary.Service.Caching
{
    public class Binance:IBinance
    {
        private readonly IBinance iBinance;
        private readonly IMemoryCache memoryCache;
        private const string memorykey = "Binance";


        public Binance(IBinance binance, IMemoryCache memoryCache)
        {
            this.iBinance = binance;
            this.memoryCache = memoryCache;
        }


        public async Task<List<SymbolData>> GetSymbols()
        {
            if (memoryCache.TryGetValue($"{memorykey}_symbols",
                    out List<SymbolData> symbols))
            {
                return symbols;
            }

            symbols = await this.iBinance.GetSymbols();
            if (symbols != null)
                memoryCache.Set(
                    $"{memorykey}_symbols", symbols,
                    new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.MaxValue }.SetPriority(
                        CacheItemPriority.NeverRemove));

            return symbols;
        }


        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            if (memoryCache.TryGetValue($"{memorykey}_{symbolData.Symbol}_trade",
                    out TradeRoot trade))
            {
                return trade;
            }

            trade = await this.iBinance.GetTrades(symbolData);
            if (trade != null)
                memoryCache.Set(
                    $"{memorykey}_{symbolData.Name}_trade", trade,
                    new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.FromUnixTimeSeconds(40) }.SetPriority(
                        CacheItemPriority.NeverRemove));

            return trade;
        }
    }
}
