using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Service
{
    public class Bigone : IBigone
    {
        private readonly CryptoArbitrageLibrary.Repository.Bigone.IBigone iBigone;


        public Bigone(CryptoArbitrageLibrary.Repository.Bigone.IBigone bigone)
        {
            this.iBigone = bigone;
        }


        public async Task<List<SymbolData>> GetSymbols()
        {
            return await iBigone.GetSymbols();
        }


        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            return await iBigone.GetTrades(symbolData);
        }
    }
}
