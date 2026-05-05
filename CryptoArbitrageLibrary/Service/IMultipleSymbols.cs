using CryptoArbitrageLibrary.Model;

namespace CryptoArbitrageLibrary.Service
{
    public interface IMultipleSymbols
    {
        public List<List<SymbolData>> GetSymbols();
    }
}
