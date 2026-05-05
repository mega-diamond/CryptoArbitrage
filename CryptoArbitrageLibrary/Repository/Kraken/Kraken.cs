using System.Text.Json;
using Microsoft.Extensions.Options;
using CryptoArbitrageLibrary.Model.Kraken.Deserialization;
using CryptoArbitrageLibrary.Model;
using System.Text.Json.Nodes;
using CryptoArbitrageLibrary.Configuration;
using Markets = CryptoArbitrageLibrary.Configuration.Markets;

namespace CryptoArbitrageLibrary.Repository.Kraken
{
    public class Kraken : IKraken
    {
        public readonly IHttpClientFactory httpClientFactory;
        private readonly RequestDetail myRequestDetail;


        public Kraken(IHttpClientFactory httpClientFactory, IOptions<Markets> marketOptions)
        {
            this.httpClientFactory = httpClientFactory;
            myRequestDetail = marketOptions?.Value?.RequestDetail?.Where(x => x.MarketName.Equals("Kraken")).First();
        }
        

        public async Task<List<SymbolData>> GetSymbols()
        {
            var res = string.Empty;
            var symbols = new List<string>();

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.DefaultRequestHeaders.ConnectionClose = false;
			client.BaseAddress = new Uri(myRequestDetail.GetSymbols);
            

            var serverResponse = await client.GetStringAsync("AssetPairs");

            var options = new JsonSerializerOptions
            {
                Converters = { new KrakenSymbolJsonConverter() }
            };

            var deserializedResponse = JsonSerializer.Deserialize<KrakenSymbolRoot>(serverResponse, options)!;

            if (deserializedResponse.error.Count > 0)
            {
                throw new Exception(deserializedResponse.error.ToString());
            }

            return deserializedResponse.result.Select(x => new SymbolData(x.Value.altname, x.Key, x.Value.wsname)).ToList();
        }




        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            var res = string.Empty;
            var symbols = new List<string>();

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.DefaultRequestHeaders.ConnectionClose = false;

			client.BaseAddress = new Uri(myRequestDetail.GetTrades!);

            var serverResponse = await client.GetStringAsync($"Trades?pair={symbolData.Symbol}");
            var symbol = symbolData.Symbol;
            var jsonResult = JsonObject.Parse(serverResponse)["result"];

            var krakenTradeRoot = new TradeRoot
            {
                Symbol = symbol,
                Result = new List<TradeDetail>()
            };

            var index = 0;

            foreach (var node in (JsonArray)jsonResult[symbolData.Symbol])
            {
                var krakenTradeDetail = new TradeDetail
                {
                    index = index,
                    price = Convert.ToDouble(node?[0]?.ToString()),
                    volume = Convert.ToDouble(node?[1]?.ToString()),
                    time = Utility.GetDateTimeFromEpoch(Convert.ToDouble(node?[2]?.ToString())),
                    buySell = node?[3]?.ToString(),
                    tradeId = Convert.ToInt64(node?[6]?.ToString())
                };

                krakenTradeRoot.Result.Add(krakenTradeDetail);
                index++;
            }

            return krakenTradeRoot;
        }
    }
}
