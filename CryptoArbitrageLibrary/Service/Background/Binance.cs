using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CryptoArbitrageLibrary.Service.Background
{
    public class Binance : BackgroundService
    {
        private readonly IServiceProvider scopeProvider;
        private readonly IMemoryCache memoryCache;
        private const string memorykey = "Binance";


        public Binance(IServiceProvider scopeProvider, IMemoryCache memoryCache)
        {
            this.scopeProvider = scopeProvider;
            this.memoryCache = memoryCache;
        }



        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var serviceScope = scopeProvider.CreateScope();

            var binance = serviceScope.ServiceProvider.GetRequiredService<Repository.Binance.IBinance>();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var symbolData = new SymbolData("BTCUSDT", "BTCUSDT_BINANCE", "BTCUSDT");

                    var listOfCashTrades = (TradeRoot)memoryCache.Get("Binance_BTCUSDT_trade")!;

                    var trade = await binance.GetTrades(symbolData);

                    if (trade != null)
                    {
                        memoryCache.Set(
                            "Binance_BTCUSDT_trade",
                            trade.Result.Any() ? trade : listOfCashTrades,
                            new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.MaxValue /* FromUnixTimeSeconds(40)*/ }.SetPriority(
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