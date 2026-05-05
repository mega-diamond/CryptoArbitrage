namespace CryptoArbitrageLibrary.Model.Bybit.Deserialization.Symbol
{
    public class BybitSymbolRoot
    {
        public int? retCode { get; set; }
        public string? retMsg { get; set; }
        public Result? result { get; set; }
        public RetExtInfo? retExtInfo { get; set; }
        public long? time { get; set; }
    }
}
