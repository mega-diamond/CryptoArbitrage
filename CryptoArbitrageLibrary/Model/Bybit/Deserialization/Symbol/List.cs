namespace CryptoArbitrageLibrary.Model.Bybit.Deserialization.Symbol
{
    public class List
    {
        public string? symbol { get; set; }
        public string? contractType { get; set; }
        public string? status { get; set; }
        public string? baseCoin { get; set; }
        public string? quoteCoin { get; set; }
        public string? launchTime { get; set; }
        public string? deliveryTime { get; set; }
        public string? deliveryFeeRate { get; set; }
        public string? priceScale { get; set; }
        public LeverageFilter? leverageFilter { get; set; }
        public PriceFilter? priceFilter { get; set; }
        public LotSizeFilter? lotSizeFilter { get; set; }
        public bool? unifiedMarginTrade { get; set; }
        public int? fundingInterval { get; set; }
        public string? settleCoin { get; set; }
        public string? copyTrading { get; set; }
        public string? upperFundingRate { get; set; }
        public string? lowerFundingRate { get; set; }
        public bool? isPreListing { get; set; }
        public object? preListingInfo { get; set; }
        public RiskParameters? riskParameters { get; set; }
        public string? displayName { get; set; }
    }
}
