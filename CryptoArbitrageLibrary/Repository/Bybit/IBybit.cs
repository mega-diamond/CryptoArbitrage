using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Repository.ByBit
{
    public interface IBybit
    {
        public Task<List<SymbolData>> GetSymbols();

        public Task<TradeRoot> GetTrades(SymbolData symbolData);
    }
}
