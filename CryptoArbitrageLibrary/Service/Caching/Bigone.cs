using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoArbitrageLibrary.Service.Caching
{
    public class Bigone:IBigone
    {
        private readonly IBigone iBigone;
        private readonly IMemoryCache memoryCache;

        private const string memorykey = "Bigone";

        public Bigone(IBigone bigone, IMemoryCache memoryCache)
        {
            this.iBigone = bigone;
            this.memoryCache = memoryCache;
        }


        public async Task<List<SymbolData>> GetSymbols()
        {
            if (memoryCache.TryGetValue($"{memorykey}_symbols",
                    out List<SymbolData> symbols))
            {
                return symbols;
            }

            symbols = await this.iBigone.GetSymbols();
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

            trade = await this.iBigone.GetTrades(symbolData);
            if (trade != null)
                memoryCache.Set(
                    $"{memorykey}_{symbolData.Name}_trade", trade,
                    new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.FromUnixTimeSeconds(40) }.SetPriority(
                        CacheItemPriority.NeverRemove));

            return trade;
        }
    }
}
