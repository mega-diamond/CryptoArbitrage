using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Repository.Binance
{
    public interface IBinance
    {
        public Task<List<SymbolData>> GetSymbols();

        public Task<TradeRoot> GetTrades(SymbolData symbolData);
    }
}
