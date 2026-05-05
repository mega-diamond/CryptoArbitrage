using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Repository.Kraken
{
    public interface IKraken
    {
        public  Task<List<SymbolData>> GetSymbols();

        public  Task<TradeRoot> GetTrades(SymbolData symbolData);
    }
}
