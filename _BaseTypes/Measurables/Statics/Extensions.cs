namespace CsabaDu.FooVaria.BaseTypes.Measurables.Statics;

public static class Extensions
{
    #region CsabaDu.FooVaria.Measurables.Enums.MeasureUnitCode
    public static TypeCode GetQuantityTypeCode(this MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode switch
        {
            MeasureUnitCode.Currency => TypeCode.Decimal,

            MeasureUnitCode.Pieces => TypeCode.Int64,

            MeasureUnitCode.AreaUnit or
            MeasureUnitCode.DistanceUnit or
            MeasureUnitCode.ExtentUnit or
            MeasureUnitCode.TimePeriodUnit or
            MeasureUnitCode.VolumeUnit or
            MeasureUnitCode.WeightUnit => TypeCode.Double,

            _ => throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode),
        };
    }

    public static string GetName(this MeasureUnitCode measureUnitCode)
    {
        return Enum.GetName(Defined(measureUnitCode, nameof(measureUnitCode)))!;
    }

    public static IEnumerable<string> GetMeasureUnitDefaultNames(this MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();

        return Enum.GetNames(measureUnitType);
    }

    public static Type GetMeasureUnitType(this MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitType(measureUnitCode);
    }

    public static IEnumerable<Enum> GetAllMeasureUnits(this MeasureUnitCode measureUnitCode)
    {
        Type measureUnitType = measureUnitCode.GetMeasureUnitType();

        foreach (Enum item in Enum.GetValues(measureUnitType))
        {
            yield return item;
        }
    }

    public static Enum GetDefaultMeasureUnit(this MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode.GetAllMeasureUnits().First(x => (int)(object)x == 0);
    }

    public static bool IsCustomMeasureUnitCode(this MeasureUnitCode measureUnitCode)
    {
        if (!Enum.IsDefined(measureUnitCode)) return false;

        Type measureUnitType = measureUnitCode.GetMeasureUnitType();
        string? defaultMeasureUnitName = Enum.GetName(measureUnitType, default(int));

        return defaultMeasureUnitName == Default;
    }

    public static bool IsSpreadMeasureUnitCode(this MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == MeasureUnitCode.AreaUnit
            || measureUnitCode == MeasureUnitCode.VolumeUnit;
    }
    #endregion
}
