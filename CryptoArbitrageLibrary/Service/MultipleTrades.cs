using System.ComponentModel;
using System.Security.Cryptography;
using CryptoArbitrageLibrary.Charts;
using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoArbitrageLibrary.Service
{
    public class MultipleTrades: IMultipleTrades
    {
        private readonly IMemoryCache memoryCache;


        public MultipleTrades(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        public List<TradeRoot> GetTrades(string symbol)
        {
            var listOfAll = new List<TradeRoot>();

            try
            {
                var listBigone = this.memoryCache.Get<TradeRoot>($"Bigone_{symbol}_trade");
                var listBinance = this.memoryCache.Get<TradeRoot>($"Binance_{symbol}_trade");
                var listBybit = this.memoryCache.Get<TradeRoot>($"Bybit_{symbol}_trade");
                var listKraken = this.memoryCache.Get<TradeRoot>($"Kraken_{symbol}_trade");

                if (listBigone == null || listBinance == null || listBybit == null || listKraken == null)
                {
                    //throw new Exception("One or more symbol lists are not available in the cache.");
                    return listOfAll; // Return an empty list if any of the symbol lists are not available
                }


                listOfAll.Add(listBigone);
                listOfAll.Add(listBinance);
                listOfAll.Add(listBybit);
                listOfAll.Add(listKraken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return listOfAll;
        }




        public Dictionary<string, TradeRoot> GetBtcUsdTrades()
        {
            var listOfAll = new Dictionary<string,TradeRoot>();

            try
            {
                var listBigone = this.memoryCache.Get<TradeRoot>($"Bigone_BTCUSDT_trade");
                var listBinance = this.memoryCache.Get<TradeRoot>($"Binance_BTCUSDT_trade");
                var listBybit = this.memoryCache.Get<TradeRoot>($"Bybit_BTCUSDT_trade");
                var listKraken = this.memoryCache.Get<TradeRoot>($"Kraken_XBTUSDT_trade");

                if (listBigone == null || listBinance == null || listBybit == null || listKraken == null)
                {
                    //throw new Exception("One or more symbol lists are not available in the cache.");
                    return listOfAll; // Return an empty list if any of the symbol lists are not available
                }


                listOfAll.Add("Bigone", listBigone);
                listOfAll.Add("Binance", listBinance);
                listOfAll.Add("Bybit", listBybit);
                listOfAll.Add("Kraken", listKraken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return listOfAll;
        }




        public Dictionary<string, List<TradeDetail>?> GetLast5BtcUsdTrades()
        {
            var listOfAll = new Dictionary<string, List<TradeDetail>?>();

            try
            {
                var listBigone = this.memoryCache.Get<TradeRoot>($"Bigone_BTC-USDT_trade");
                var listBinance = this.memoryCache.Get<TradeRoot>($"Binance_BTCUSDT_trade");
                var listBybit = this.memoryCache.Get<TradeRoot>($"Bybit_BTCUSDT_trade");
                var listKraken = this.memoryCache.Get<TradeRoot>($"Kraken_XBTUSDT_trade");

                listOfAll.Add("Bigone", listBigone?.Result?.OrderByDescending(x=>x.time).Take(5).ToList());
                listOfAll.Add("Binance", listBinance?.Result?.OrderByDescending(x => x.time).Take(5).ToList());
                listOfAll.Add("Bybit", listBybit?.Result?.OrderByDescending(x => x.time).Take(5).ToList());
                listOfAll.Add("Kraken", listKraken?.Result?.OrderByDescending(x => x.time).Take(5).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return listOfAll;
        }


        public Dictionary<string, TradeDetail?> GetLastBtcUsdTrades()
        {
            var listOfAll = new Dictionary<string, TradeDetail?>();

            try
            {
                var listBigone = this.memoryCache.Get<TradeRoot>($"Bigone_BTCUSDT_trade");
                var listBinance = this.memoryCache.Get<TradeRoot>($"Binance_BTCUSDT_trade");
                var listBybit = this.memoryCache.Get<TradeRoot>($"Bybit_BTCUSDT_trade");
                var listKraken = this.memoryCache.Get<TradeRoot>($"Kraken_XBTUSDT_trade");

                listOfAll.Add("Bigone", listBigone?.Result?.OrderByDescending(x => x.time).First());
                listOfAll.Add("Binance", listBinance?.Result?.OrderByDescending(x => x.time).First());
                listOfAll.Add("Bybit", listBybit?.Result?.OrderByDescending(x => x.time).First());
                listOfAll.Add("Kraken", listKraken?.Result?.Last());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return listOfAll;
        }



        public string GetLastChartsBtcUsdPrice()
        {
            var chart = string.Empty;

            try
            {
                var listBigone = this.memoryCache.Get<TradeRoot>($"Bigone_BTCUSDT_trade");
                var listBinance = this.memoryCache.Get<TradeRoot>($"Binance_BTCUSDT_trade");
                var listBybit = this.memoryCache.Get<TradeRoot>($"Bybit_BTCUSDT_trade");
                var listKraken = this.memoryCache.Get<TradeRoot>($"Kraken_XBTUSDT_trade");

                var bigone = listBigone?.Result?.OrderByDescending(x => x.time).First();
                var binance = listBinance?.Result?.OrderByDescending(x => x.time).First();
                var bybit = listBybit?.Result?.OrderByDescending(x => x.time).First();
                var kraken = listKraken?.Result?.Last();

                var listOfPostions = new List<Position>
                {
                    new Position()
                    {
                        Caption = $"Bigone",
                        Color = "eb3d34",
                        Id = 1,
                        X = bigone?.price ?? 0,
                        Y = bigone?.time ?? DateTime.UtcNow 
                    },
                    new Position()
                    {
                        Caption = $"Binance",
                        Color = "343deb",
                        Id = 1,
                        X = binance?.price ?? 0,
                        Y = binance?.time ?? DateTime.UtcNow
                    },
                    new Position()
                    {
                        Caption = $"Bybit",
                        Color = "43a32f",
                        Id = 1,
                        X = bybit?.price ?? 0,
                        Y = bybit?.time ?? DateTime.UtcNow
                    },
                    new Position()
                    {
                        Caption = $"Kraken",
                        Color = "a32f9f",
                        Id = 1,
                        X = kraken?.price ?? 0,
                        Y = kraken?.time ?? DateTime.UtcNow
                    }
                };

                chart = BarChartRepository.GetHorizontalBarChart(listOfPostions);
            }
            catch (Exception ex)
            {
               throw;
            }

            return chart;
        }
    }
}
