namespace CryptoArbitrageLibrary.Model.Kraken.Deserialization
{
    public class KrakenSymbolRoot
    {
        public List<object>? error { get; set; }
        public Dictionary<string, KrakenSymbolDetail>? result { get; set; }
    }
}
