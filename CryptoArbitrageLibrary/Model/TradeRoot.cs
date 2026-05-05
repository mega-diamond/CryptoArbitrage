namespace CryptoArbitrageLibrary.Model
{
    public class TradeRoot
    {
        public string? Symbol { get; set; }
        public List<TradeDetail>? Result { get; set; }
    }
}
