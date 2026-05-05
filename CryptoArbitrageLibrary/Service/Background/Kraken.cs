using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoArbitrageLibrary.Service.Background
{
    public class Kraken:BackgroundService
    {
        private readonly IServiceProvider scopeProvider;
        private readonly IMemoryCache memoryCache;
        private const string memorykey = "Kraken";



        public Kraken(IServiceProvider scopeProvider, IMemoryCache memoryCache)
        {
            this.scopeProvider = scopeProvider;
            this.memoryCache = memoryCache;
        }
        


        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var serviceScope = scopeProvider.CreateScope();
            var kraken = serviceScope.ServiceProvider.GetRequiredService<Repository.Kraken.IKraken>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var symbolData = new SymbolData("BTCUSDT", "BTCUSDT_KRAKEN", "XBTUSDT");

                    var listOfCashTrades = (TradeRoot)memoryCache.Get("Kraken_XBTUSDT_trade")!;

                    var trade = await kraken.GetTrades(symbolData);


                    var lastTime = trade.Result.Select(x=>x.time).Last();



                    if (trade != null)
                    {
                        memoryCache.Set(
                            "Kraken_XBTUSDT_trade",
                            trade.Result.Any() ? trade : listOfCashTrades,
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