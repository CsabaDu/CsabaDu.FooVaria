namespace CsabaDu.FooVaria.Tests.TestHelpers.Params
{
    public sealed class TestSupport
    {
        private static readonly RandomParams RandomParams = new();

        public static bool Returned(Action validator)
        {
            try
            {
                validator();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Enum GetSameTypeValidMeasureUnit(Enum measureUnit)
        {
            if (!IsValidMeasureUnit(measureUnit)) throw InvalidMeasureUnitEnumArgumentException(measureUnit);

            MeasureUnitCode measureUnitCode = GetMeasureUnitCode(measureUnit);
            bool isCustomMeasureUnit = IsCustomMeasureUnit(measureUnit);

            if (isCustomMeasureUnit)
            {
                string customame = RandomParams.GetRandomParamName();
                decimal exchangeRate = RandomParams.GetRandomPositiveDecimal();

                SetCustomMeasureUnit(customame, measureUnitCode, exchangeRate);
            }

            return isCustomMeasureUnit ?
                (Enum)Enum.ToObject(measureUnit.GetType(), 1)
                : RandomParams.GetRandomMeasureUnit(measureUnitCode);
        }

        public static void RestoreConstantExchangeRates()
        {
            if (ExchangeRateCollection.Count != ConstantExchangeRateCount)
            {
                BaseMeasurement.RestoreConstantExchangeRates();
            }
        }
    }
}
