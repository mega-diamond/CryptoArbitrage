using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoArbitrageLibrary.Service.Caching
{
    public class Kraken:IKraken
    {
        private readonly IKraken iKraken;
        private readonly IMemoryCache memoryCache;
        private const string memorykey = "Kraken";
        

        public Kraken(IKraken kraken, IMemoryCache memoryCache)
        {
            this.iKraken = kraken;
            this.memoryCache = memoryCache;
        }
        
                  
        public async Task<List<SymbolData>> GetSymbols()
        {
            if (memoryCache.TryGetValue($"{memorykey}_symbols",
                    out List<SymbolData> symbols))
            {
                return symbols;
            }
        
            symbols = await this.iKraken.GetSymbols();
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
        
            trade = await this.iKraken.GetTrades(symbolData);
            if (trade != null)
                memoryCache.Set(
                    $"{memorykey}_{symbolData.Symbol}_trade", trade,
                    new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.FromUnixTimeSeconds(40) }.SetPriority(
                        CacheItemPriority.NeverRemove));
        
            return trade;
        }
    }
}
