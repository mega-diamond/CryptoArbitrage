using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Options;
using System.Text.Json;
using CryptoArbitrageLibrary.Model.Bigone.Deserialization.Symbol;
using Markets = CryptoArbitrageLibrary.Configuration.Markets;
using CryptoArbitrageLibrary.Configuration;


namespace CryptoArbitrageLibrary.Repository.Bigone
{
    public class Bigone : IBigone
    {
        public readonly IHttpClientFactory httpClientFactory;
        private readonly RequestDetail myRequestDetail;


        public Bigone(IHttpClientFactory httpClientFactory, IOptions<Markets> marketOptions)
        {
            this.httpClientFactory = httpClientFactory;
            myRequestDetail = marketOptions?.Value?.RequestDetail?.Where(x => x.MarketName.Equals("Bigone")).First();
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

            var serverResponse = await client.GetStringAsync("asset_pairs/tickers");

            var deserializedResponse = JsonSerializer.Deserialize<Root>(serverResponse)!;

            if (!deserializedResponse.data.Any())
            {
                throw new Exception("There is no symbols");
            }

            return deserializedResponse.data.Select(x =>
                new SymbolData(x.asset_pair_name, x.asset_pair_name.Replace("-", string.Empty), x.asset_pair_name.Replace('-','/'))).ToList();
        }



        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            var res = string.Empty;
            var symbols = new List<string>();

            var client = httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(myRequestDetail.GetTrades!);
            client.DefaultRequestHeaders.ConnectionClose = false;

            //https://big.one/api/v3/asset_pairs/BTC-USDT/trades
            var serverResponse = await client.GetStringAsync($"asset_pairs/{symbolData.Symbol}/trades");

            var myDeserializedClass = JsonSerializer.Deserialize<Model.Bigone.Deserialization.Trades.Root>(serverResponse);

            var bigoneTradeRoot = new TradeRoot
            {
                Symbol = symbolData.Name,
                Result = new List<TradeDetail>()
            };

            var index = 0;

            foreach (var node in myDeserializedClass.data)
            {
                var binanceTradeDetail = new TradeDetail
                {
                    index = index,
                    price = Convert.ToDouble(node.price),
                    volume = Convert.ToDouble(node.amount),
                    time = node.created_at,
                    tradeId = node.id
                };

                bigoneTradeRoot.Result.Add(binanceTradeDetail);
                index++;
            }

            return bigoneTradeRoot;
        }
    }
}