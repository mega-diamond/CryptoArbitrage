namespace CryptoArbitrageLibrary.Model.Bybit.Deserialization.Trade
{
    public class RootTrade
    {
        public int? retCode { get; set; }
        public string? retMsg { get; set; }
        public Result? result { get; set; }
        public RetExtInfo? retExtInfo { get; set; }
        public long? time { get; set; }
    }
}
