using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoArbitrageLibrary.Service.Background
{
    public class Bigone:BackgroundService
    {
        private readonly IServiceProvider scopeProvider;
        private readonly IMemoryCache memoryCache;
        private const string memorykey = "Bigone";


        public Bigone(IServiceProvider scopeProvider, IMemoryCache memoryCache)
        {
            this.scopeProvider = scopeProvider;
            this.memoryCache = memoryCache;
        }        


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var serviceScope = scopeProvider.CreateScope();

           var bigone = serviceScope.ServiceProvider.GetRequiredService<Repository.Bigone.IBigone>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var symbolData = new SymbolData("BTC-USDT", "BTCUSDT_BIGONE", "BTC-USDT");

                    var listOfCashTrades = (TradeRoot)memoryCache.Get("Bigone_BTCUSDT_trade")!;

                    var trade = await bigone.GetTrades(symbolData);
                   

                    if (trade != null)
                    {
                        memoryCache.Set(
                            "Bigone_BTCUSDT_trade",
                            trade.Result.Any()  ? trade : listOfCashTrades,
                            new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.MaxValue /*.FromUnixTimeSeconds(40)*/ }.SetPriority(
                                CacheItemPriority.NeverRemove));
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }

        }
    }
}
