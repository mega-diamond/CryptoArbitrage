namespace CryptoArbitrageLibrary.Repository
{
    public static class Utility
    {
        public static DateTime GetDateTimeFromEpoch(double epoch)
        {
            var dtoDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            switch (epoch)
            {
                // micro seconds
                case > 1000000000000000:
                    dtoDateTime = dtoDateTime.AddTicks((long)epoch * 10);
                    break;
                // mili seconds
                case > 1000000000000:
                    dtoDateTime = dtoDateTime.AddMilliseconds(epoch);
                    break;
                // seconds
                default:
                    dtoDateTime = dtoDateTime.AddSeconds(epoch);
                    break;
            }

            return dtoDateTime;
        }
    }
}
