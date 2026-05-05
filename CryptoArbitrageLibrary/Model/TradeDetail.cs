namespace CryptoArbitrageLibrary.Model
{
    public class TradeDetail
    {
        public long? index { get; set; }

        public double? price { get; set; }

        public double? volume { get; set; }

        public DateTime? time { get; set; }

        public string? buySell { get; set; }

        public double? tradeId { get; set; }
    }
}
