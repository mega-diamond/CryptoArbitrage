namespace CryptoArbitrageLibrary.Model.Bybit.Deserialization.Symbol
{
    public class Result
    {
        public string? category { get; set; }
        public List<List>? list { get; set; }
        public string? nextPageCursor { get; set; }
    }
}
