namespace CryptoArbitrageLibrary.Model.Bigone.Deserialization.Symbol
{
    public class Datum
    {
        public string? asset_pair_name { get; set; }
        public AskBid? bid { get; set; }
        public AskBid? ask { get; set; }
        public string? open { get; set; }
        public string? high { get; set; }
        public string? low { get; set; }
        public string? close { get; set; }
        public string? volume { get; set; }
        public string? daily_change { get; set; }
    }
}
