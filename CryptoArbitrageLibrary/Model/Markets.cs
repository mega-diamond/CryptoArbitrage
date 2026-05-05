using Microsoft.Extensions.Options;

namespace CryptoArbitrageLibrary.Model
{
    public class Markets : IMarkets
    {
        private List<string> markets;
        private readonly CryptoArbitrageLibrary.Configuration.Markets myConfig;
        

        public Markets(IOptions<CryptoArbitrageLibrary.Configuration.Markets> myConfig)
        {
            markets = new List<string>();
            this.myConfig = myConfig.Value;
            foreach (var market in this.myConfig.RequestDetail)
            {
                markets.Add(market.MarketName);
            }
        }        


        public List<string> GetMarkets()
        {
            return markets;
        }
    }
}
