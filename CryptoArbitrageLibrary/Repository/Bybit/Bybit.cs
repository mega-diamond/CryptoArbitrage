using CryptoArbitrageLibrary.Model;
using CryptoArbitrageLibrary.Model.Bybit.Deserialization.Symbol;
using Microsoft.Extensions.Options;
using System.Text.Json;
using CryptoArbitrageLibrary.Configuration;
using CryptoArbitrageLibrary.Model.Bybit.Deserialization.Trade;
using Markets = CryptoArbitrageLibrary.Configuration.Markets;

namespace CryptoArbitrageLibrary.Repository.ByBit
{
    public class Bybit : IBybit
    {
        public readonly IHttpClientFactory httpClientFactory;
        private readonly RequestDetail myRequestDetail;


        public Bybit(IHttpClientFactory httpClientFactory, IOptions<Markets> marketOptions)
        {
            this.httpClientFactory = httpClientFactory;
            myRequestDetail = marketOptions?.Value?.RequestDetail?.Where(x => x.MarketName.Equals("Bybit")).First();
        }



        public async Task<List<SymbolData>> GetSymbols()
        {
            var res = string.Empty;
            var symbols = new List<string>();

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.DefaultRequestHeaders.ConnectionClose = false;
            client.BaseAddress = new Uri(myRequestDetail.GetSymbols);
            client.DefaultRequestHeaders.ConnectionClose = false;

            var serverResponse = await client.GetStringAsync("v5/market/instruments-info?category=linear");

            var deserializedResponse = JsonSerializer.Deserialize<BybitSymbolRoot>(serverResponse)!;

            if (!deserializedResponse.result.list.Any())
            {
                throw new Exception("There is no symbols");
            }

            return deserializedResponse.result.list.Where(x => !IsContainingDate(x.symbol)).Select(x => new SymbolData(x.symbol, x.symbol, $"{x.baseCoin}/{x.quoteCoin}")).ToList();
        }



        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            var res = string.Empty;
            var symbols = new List<string>();

            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(myRequestDetail.GetTrades!);
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.DefaultRequestHeaders.ConnectionClose = false;

            var serverResponse = await client.GetStringAsync($"v5/market/recent-trade?symbol={symbolData.Symbol}");
            
            var myDeserializedClass = JsonSerializer.Deserialize<RootTrade>(serverResponse);

            var bybitTradeRoot = new TradeRoot
            {
                Symbol = symbolData.Name,
                Result = new List<TradeDetail>()
            };

            var index = 0;

            foreach (var node in myDeserializedClass.result.list)
            {
                var binanceTradeDetail = new TradeDetail
                {
                    index = index,
                    price = Convert.ToDouble(node.price),
                    volume = Convert.ToDouble(node.size),
                    time = Utility.GetDateTimeFromEpoch(Convert.ToDouble(node.time)),
                    tradeId = Math.Abs(node.execId.GetHashCode())
                };

                bybitTradeRoot.Result.Add(binanceTradeDetail);
                index++;
            }

            return bybitTradeRoot;
        }


        public static bool IsContainingDate(string symbol)
        {
            return symbol.Contains("-", StringComparison.CurrentCulture);
        }
    }
}
