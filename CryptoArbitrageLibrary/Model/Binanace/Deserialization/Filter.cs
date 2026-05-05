namespace CryptoArbitrageLibrary.Model.Binanace.Deserialization
{
    public class Filter
    {
        public string? filterType { get; set; }
        public string? minPrice { get; set; }
        public string? maxPrice { get; set; }
        public string? tickSize { get; set; }
        public string? minQty { get; set; }
        public string? maxQty { get; set; }
        public string? stepSize { get; set; }
        public int? limit { get; set; }
        public int? minTrailingAboveDelta { get; set; }
        public int? maxTrailingAboveDelta { get; set; }
        public int? minTrailingBelowDelta { get; set; }
        public int? maxTrailingBelowDelta { get; set; }
        public string? bidMultiplierUp { get; set; }
        public string? bidMultiplierDown { get; set; }
        public string? askMultiplierUp { get; set; }
        public string? askMultiplierDown { get; set; }
        public int? avgPriceMins { get; set; }
        public string? minNotional { get; set; }
        public bool? applyMinToMarket { get; set; }
        public string? maxNotional { get; set; }
        public bool? applyMaxToMarket { get; set; }
        public int? maxNumOrders { get; set; }
        public int? maxNumAlgoOrders { get; set; }
    }
}
