namespace NewsPortal.Infrastucture.Extensions
{
    public static class ConvertExtensions
    {
        public static int ConvertToInt32WithDefaultValue(string? str, int defaultValue)
        {
            var convertedValue = Convert.ToInt32(str);
            if (convertedValue == 0)
            {
                return defaultValue;
            }

            return convertedValue;
        }
    }
}
