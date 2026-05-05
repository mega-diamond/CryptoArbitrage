using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Service
{
    public interface IBigone
    {
        public Task<List<SymbolData>> GetSymbols();

        public Task<TradeRoot> GetTrades(SymbolData symbolData);
    }
}
