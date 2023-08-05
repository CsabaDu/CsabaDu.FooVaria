namespace CsabaDu.FooVaria.Common.Statics
{
    public static class Validate
    {
        public static object NullChecked(object? obj, string name)
        {
            return obj ?? throw new ArgumentNullException(name);
        }

        public static InvalidEnumArgumentException InvalidMeasureUnitEnumArgumentException(Enum measureUnit)
        {
            return new InvalidEnumArgumentException(nameof(measureUnit), (int)(object)measureUnit, measureUnit.GetType());
        }

        public static InvalidEnumArgumentException InvalidMeasureUnitTypeCodeEnumArgumentException(MeasureUnitTypeCode measureUnitTypeCode)
        {
            return new InvalidEnumArgumentException(nameof(measureUnitTypeCode), (int)measureUnitTypeCode, measureUnitTypeCode.GetType());
        }

        public static ArgumentOutOfRangeException ExchangeRateArgumentOutOfRangeException(decimal? exchangeRate)
        {
            return new ArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate, null);
        }
    }
}
