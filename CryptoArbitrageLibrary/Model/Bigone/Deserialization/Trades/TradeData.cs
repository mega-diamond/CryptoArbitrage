namespace CryptoArbitrageLibrary.Model.Bigone.Deserialization.Trades
{
    public class TradeData
    {
        public long? id { get; set; }
        public string? price { get; set; }
        public string? amount { get; set; }
        public string? taker_side { get; set; }
        public DateTime? inserted_at { get; set; }
        public DateTime? created_at { get; set; }
    }
}
