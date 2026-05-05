using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Service
{
    public class Binance : IBinance
    {
        private readonly CryptoArbitrageLibrary.Repository.Binance.IBinance iBinance;
        

        public Binance(CryptoArbitrageLibrary.Repository.Binance.IBinance binance)
        {
            this.iBinance = binance;
        }


        public async Task<List<SymbolData>> GetSymbols()
        {
            return await iBinance.GetSymbols();
        }


        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            return await iBinance.GetTrades(symbolData);
        }
    }
}
