namespace CsabaDu.FooVaria.Measurements.Statics
{
    //public static class MeasureUnits
    //{
    //    #region Public methods
    //    public static IEnumerable<object> GetConstantMeasureUnits()
    //    {
    //        foreach (object item in ConstantExchangeRateCollection.Keys)
    //        {
    //            yield return item;
    //        }

    //    }

    //    public static decimal GetExchangeRate(Enum measureUnit)
    //    {
    //        decimal exchangeRate = ExchangeRateCollection.FirstOrDefault(x => x.Key.Equals(measureUnit)).Value;

    //        if (exchangeRate != default) return exchangeRate;

    //        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    //    }

    //    public static IEnumerable<object> GetValidMeasureUnits()
    //    {
    //        foreach (object item in ExchangeRateCollection.Keys)
    //        {
    //            yield return item;
    //        }
    //    }

    //    public static bool IsCustomMeasureUnit(Enum measureUnit)
    //    {
    //        if (!IsDefinedMeasureUnit(measureUnit)) return false;

    //        MeasureUnitTypeCode measureUnitTypeCode = GetMeasureUnitTypeCode(measureUnit);

    //        return measureUnitTypeCode.IsCustomMeasureUnitTypeCode();
    //    }

    //    public static bool IsValidMeasureUnit(Enum? measureUnit)
    //    {
    //        if (measureUnit == null) return false;

    //        return GetValidMeasureUnits().Contains(measureUnit);
    //    }
    //    #endregion
    //}
}
