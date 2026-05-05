using CryptoArbitrageLibrary.Model;
using CryptoArbitrageLibrary.Model.Binanace.Deserialization;
using Microsoft.Extensions.Options;
using System.Text.Json;
using CryptoArbitrageLibrary.Configuration;
using Markets = CryptoArbitrageLibrary.Configuration.Markets;

namespace CryptoArbitrageLibrary.Repository.Binance
{
    public class Binance : IBinance
    {
        public readonly IHttpClientFactory httpClientFactory;
        private readonly RequestDetail myRequestDetail;


        public Binance(IHttpClientFactory httpClientFactory, IOptions<Markets> marketOptions)
        {
            this.httpClientFactory = httpClientFactory;
			myRequestDetail = marketOptions?.Value?.RequestDetail?.Where(x => x.MarketName.Equals("Binance")).First();
        }



        public async Task<List<SymbolData>> GetSymbols()
        {
            var res = string.Empty;
            var symbols = new List<string>();

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.DefaultRequestHeaders.ConnectionClose = false;
			client.BaseAddress = new Uri(myRequestDetail.GetSymbols);
          

            var serverResponse = await client.GetStringAsync("api/v3/exchangeInfo");            

            var deserializedResponse = JsonSerializer.Deserialize<BinanceSymbolRoot>(serverResponse)!;

            if (!deserializedResponse.symbols.Any())
            {
                throw new Exception("There is no symbols");
            }

            return deserializedResponse.symbols.Select(x => new SymbolData(x.symbol, x.symbol, $"{x.baseAsset}/{x.quoteAsset}" )).ToList();
        }

 

        public async Task<TradeRoot> GetTrades(SymbolData symbolData)
        {
            var res = string.Empty;
            var symbols = new List<string>();

            var client = httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
            client.DefaultRequestHeaders.ConnectionClose = false;

			client.BaseAddress = new Uri(myRequestDetail.GetTrades!);
           

            var serverResponse = await client.GetStringAsync($"api/v3/trades?symbol={symbolData.Symbol}");

            var myDeserializedClass = JsonSerializer.Deserialize<List<Trade>>(serverResponse);

            var binanceTradeRoot = new TradeRoot
            {
                Symbol = symbolData.Name,
                Result = new List<TradeDetail>()
            };

            var index = 0;

            foreach (var node in myDeserializedClass)
            {
                var binanceTradeDetail = new TradeDetail
                {
                    index = index,
                    price = Convert.ToDouble(node.price),
                    volume = Convert.ToDouble(node.qty),
                    time = Utility.GetDateTimeFromEpoch(Convert.ToDouble(node.time)),
                    tradeId = node.id
                };

                binanceTradeRoot.Result.Add(binanceTradeDetail);
                index++;
            }

            return binanceTradeRoot;
        }
    }
}
