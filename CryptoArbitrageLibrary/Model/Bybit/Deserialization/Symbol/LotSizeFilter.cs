namespace CryptoArbitrageLibrary.Model.Bybit.Deserialization.Symbol
{
    public class LotSizeFilter
    {
        public string? maxOrderQty { get; set; }
        public string? minOrderQty { get; set; }
        public string? qtyStep { get; set; }
        public string? postOnlyMaxOrderQty { get; set; }
        public string? maxMktOrderQty { get; set; }
        public string? minNotionalValue { get; set; }
    }
}
