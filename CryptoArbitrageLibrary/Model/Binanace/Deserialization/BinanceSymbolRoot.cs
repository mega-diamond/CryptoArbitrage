namespace CryptoArbitrageLibrary.Model.Binanace.Deserialization
{
    public class BinanceSymbolRoot
    {
        public string? timezone { get; set; }
        public long? serverTime { get; set; }
        public List<RateLimit>? rateLimits { get; set; }
        public List<object>? exchangeFilters { get; set; }
        public List<Symbol>? symbols { get; set; }
    }
}
