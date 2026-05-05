using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Service
{
    public class Kraken : IKraken
    {
        private readonly CryptoArbitrageLibrary.Repository.Kraken.IKraken iKraken;


        public Kraken(CryptoArbitrageLibrary.Repository.Kraken.IKraken kraken)
        {
            this.iKraken = kraken;
        }
        

        public async Task<List<SymbolData>> GetSymbols()
        {
            return await iKraken.GetSymbols();
        }


        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            return await iKraken.GetTrades(symbolData);
        }
    }
}
