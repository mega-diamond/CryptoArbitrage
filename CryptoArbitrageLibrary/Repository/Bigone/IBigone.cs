using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Repository.Bigone
{
    public interface IBigone
    {
        public Task<List<SymbolData>> GetSymbols();

        public Task<TradeRoot> GetTrades(SymbolData symbolData);
    }
}
