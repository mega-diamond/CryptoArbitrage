namespace CryptoArbitrageLibrary.Model
{
    public class SymbolData(string name, string instrument, string symbol)
    {
        public string Name { get; set; } = name;
        public string Instrument { get; set; } = instrument;
        public string Symbol { get; set; } = symbol;
    }
}
