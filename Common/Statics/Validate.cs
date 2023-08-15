using System.Security.AccessControl;

namespace CsabaDu.FooVaria.Common.Statics
{
    public static class Validate
    {
        //public static object NullChecked(object? param, string name)
        //{
        //    return param ?? throw new ArgumentNullException(name);
        //}

        public static T NullChecked<T>(T? param, string name)
        {
            return param ?? throw new ArgumentNullException(name);
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
        public static ArgumentOutOfRangeException CustomNameArgumentOutOfRangeException(string? customName)
        {
            throw new ArgumentOutOfRangeException(nameof(customName), customName, null);
        }

        public static ArgumentOutOfRangeException QuantityArgumentOutOfRangeException(ValueType? quantity)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), Type.GetTypeCode(quantity?.GetType()), null);
        }

    }
}
