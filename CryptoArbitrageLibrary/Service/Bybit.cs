using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Service
{
    public class Bybit : IBybit
    {
        private readonly CryptoArbitrageLibrary.Repository.ByBit.IBybit iBybit;
        

        public Bybit(CryptoArbitrageLibrary.Repository.ByBit.IBybit bybit)
        {
            this.iBybit = bybit;
        }


        public async Task<List<SymbolData>> GetSymbols()
        {
            return await iBybit.GetSymbols();
        }


        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            return await iBybit.GetTrades(symbolData);
        }
    }
}
