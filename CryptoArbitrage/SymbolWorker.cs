using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Options;

namespace CryptoArbitrage
{
    public class SymbolWorker : BackgroundService
    {
        private readonly CryptoArbitrageLibrary.Service.IBigone bigone;
        private readonly CryptoArbitrageLibrary.Service.IBinance binnace;
        private readonly CryptoArbitrageLibrary.Service.IBybit bybit;
        private readonly CryptoArbitrageLibrary.Service.IKraken kraken;


        private readonly IServiceProvider scopeProvider;


        public SymbolWorker(IServiceProvider scopeProvider, IOptions<CryptoArbitrageLibrary.Configuration.Markets> rootOptions)
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
                    var bigoneSymbol = await bigoneScopedService.GetSymbols();
                    var binanceSymbol = await binanceScopedService.GetSymbols();
                    var bybitSymbol = await bybitScopedService.GetSymbols();
                    var krakenSymbol = await krakenDcopedService.GetSymbols();



                    await Task.Delay(300000, stoppingToken);
                }
            }
        }
    }
}
