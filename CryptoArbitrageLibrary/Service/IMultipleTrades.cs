using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Service
{
    public interface IMultipleTrades
    {
        public List<TradeRoot> GetTrades(string symbol);


        public Dictionary<string,TradeRoot> GetBtcUsdTrades();


        public Dictionary<string, List<TradeDetail>?> GetLast5BtcUsdTrades();


        public Dictionary<string, TradeDetail?> GetLastBtcUsdTrades();


        public string GetLastChartsBtcUsdPrice();
    }
}
