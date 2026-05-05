using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Options;

namespace CryptoArbitrage
{
    public class TradeWorker : BackgroundService
    {
        private readonly CryptoArbitrageLibrary.Service.IBigone bigone;
        private readonly CryptoArbitrageLibrary.Service.IBinance binnace;
        private readonly CryptoArbitrageLibrary.Service.IBybit bybit;
        private readonly CryptoArbitrageLibrary.Service.IKraken kraken;


        private readonly IServiceProvider scopeProvider;
        

        public TradeWorker(IServiceProvider scopeProvider, IOptions<CryptoArbitrageLibrary.Configuration.Markets> rootOptions)
        {
            this.scopeProvider = scopeProvider;
        }



        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = scopeProvider.CreateScope())
            {
                var scopedService = scope.ServiceProvider.GetRequiredService<IMarkets>();

                var bigoneScopedService = scope.ServiceProvider.GetRequiredService<CryptoArbitrageLibrary.Service.IBigone>();
                var binanceScopedService = scope.ServiceProvider.GetRequiredService<CryptoArbitrageLibrary.Service.IBinance>();
                var bybitScopedService = scope.ServiceProvider.GetRequiredService<CryptoArbitrageLibrary.Service.IBybit>();
                var krakenDcopedService = scope.ServiceProvider.GetRequiredService<CryptoArbitrageLibrary.Service.IKraken>();
                

                while (!stoppingToken.IsCancellationRequested)
                {
                    var bigone = await bigoneScopedService.GetTrades(new SymbolData("BTC-USDT", "BTCUSDT_BIGONE", "BTC-USDT"));
                    var binance = await binanceScopedService.GetTrades(new SymbolData("BTCUSDT", "BTCUSDT_BINANCE", "BTCUSDT"));
                    var bybit = await bybitScopedService.GetTrades(new SymbolData("BTCUSDT", "BTCUSDT_BYBIT", "BTCUSDT"));
                    var kraken = await krakenDcopedService.GetTrades(new SymbolData("BTCUSDT", "BTCUSDT_KRAKEN", "XBTUSDT"));
                    
                    await Task.Delay(30000, stoppingToken);
                }
            }
        }
    }
}
