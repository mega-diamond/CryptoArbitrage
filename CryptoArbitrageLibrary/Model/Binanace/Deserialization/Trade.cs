namespace CryptoArbitrageLibrary.Model.Binanace.Deserialization
{
    public class Trade
    {
        public long? id { get; set; }
        public string? price { get; set; }
        public string? qty { get; set; }
        public string? quoteQty { get; set; }
        public long? time { get; set; }
        public bool? isBuyerMaker { get; set; }
        public bool? isBestMatch { get; set; }
    }
}
