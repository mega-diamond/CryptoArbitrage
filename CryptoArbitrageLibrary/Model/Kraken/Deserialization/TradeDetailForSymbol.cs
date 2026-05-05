namespace CryptoArbitrageLibrary.Model.Kraken.Deserialization
{
    public class TradeDetailForSymbol
    {
        public Dictionary<string, object[]>? Values { get; set; }
        public string? Last { get; set; }
    }
}
