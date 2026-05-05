using CryptoArbitrageLibrary.Model;
using Microsoft.Extensions.Caching.Memory;

namespace CryptoArbitrageLibrary.Service
{
    public class MultipleSymbols: IMultipleSymbols
    {
        private readonly IMemoryCache memoryCache;
        

        public MultipleSymbols(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        public List<List<SymbolData>> GetSymbols()
        {
            var listOfAll = new List<List<SymbolData>>();

            try
            {
                var listBigone = this.memoryCache.Get<List<SymbolData>>("Bigone_symbols");
                var listBinance = this.memoryCache.Get<List<SymbolData>>("Binance_symbols");
                var listBybit = this.memoryCache.Get<List<SymbolData>>("Bybit_symbols");
                var listKraken = this.memoryCache.Get<List<SymbolData>>("Kraken_symbols");

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
    }
}
