namespace CryptoArbitrageLibrary.Model.Binanace.Deserialization
{
    public class Symbol
    {
        public string? symbol { get; set; }
        public string? status { get; set; }
        public string? baseAsset { get; set; }
        public int? baseAssetPrecision { get; set; }
        public string? quoteAsset { get; set; }
        public int? quotePrecision { get; set; }
        public int? quoteAssetPrecision { get; set; }
        public int? baseCommissionPrecision { get; set; }
        public int? quoteCommissionPrecision { get; set; }
        public List<string>? orderTypes { get; set; }
        public bool? icebergAllowed { get; set; }
        public bool? ocoAllowed { get; set; }
        public bool? otoAllowed { get; set; }
        public bool? quoteOrderQtyMarketAllowed { get; set; }
        public bool? allowTrailingStop { get; set; }
        public bool? cancelReplaceAllowed { get; set; }
        public bool? amendAllowed { get; set; }
        public bool? isSpotTradingAllowed { get; set; }
        public bool? isMarginTradingAllowed { get; set; }
        public List<Filter>? filters { get; set; }
        public List<object>? permissions { get; set; }
        public List<List<string>>? permissionSets { get; set; }
        public string? defaultSelfTradePreventionMode { get; set; }
        public List<string>? allowedSelfTradePreventionModes { get; set; }

    }
}
