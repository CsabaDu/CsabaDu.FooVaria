using static CsabaDu.FooVaria.Measurements.Types.Implementations.BaseMeasurement;

namespace CsabaDu.FooVaria.Measurements.Statics
{
    public static class MeasureUnits
    {
        #region Public methods
        public static IEnumerable<object> GetConstantMeasureUnits()
        {
            foreach (object item in ConstantExchangeRateCollection.Keys)
            {
                yield return item;
            }

        }

        public static decimal GetExchangeRate(Enum measureUnit)
        {
            decimal? exchangeRate = getExchangeRate(NullChecked(measureUnit, nameof(measureUnit)));

            if (exchangeRate != null) return exchangeRate.Value;

            throw InvalidMeasureUnitEnumArgumentException(measureUnit);

            #region Local methods
            static decimal? getExchangeRate(Enum measureUnit)
            {
                decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

                if (exchangeRate == default) return null;

                return exchangeRate;
            }
            #endregion
        }

        public static IEnumerable<object> GetValidMeasureUnits()
        {
            foreach (object item in ExchangeRateCollection.Keys)
            {
                yield return item;
            }
        }

        public static bool IsValidMeasureUnit(Enum? measureUnit)
        {
            if (measureUnit == null) return false;

            return GetValidMeasureUnits().Contains(measureUnit);
        }
        #endregion
    }
}
