namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public static class ParamNames
{
    public const string baseMeasurement = nameof(baseMeasurement);
    public const string commonBase = nameof(commonBase);
    public const string context = nameof(context);
    public const string denominator = nameof(denominator);
    public const string factory = nameof(factory);
    public const string measurable = nameof(measurable);
    public const string measureUnit = nameof(measureUnit);
    public const string measureUnitCode = nameof(measureUnitCode);
    public const string numerator = nameof(numerator);
    public const string other = nameof(other);
    public const string param = nameof(param);
    public const string paramName = nameof(paramName);
    public const string quantity = nameof(quantity);
    public const string quantityTypeCode = nameof(quantityTypeCode);
    public const string rateComponentCode = nameof(rateComponentCode);
    public const string rootObject = nameof(rootObject);
    public const string roundingMode = nameof(roundingMode);
    public const string spreadMeasure = nameof(spreadMeasure);

    public static IEnumerable<string> GetParamNames()
    {
        Type type = typeof(ParamNames);
        FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

        return fields.Select(x => x.Name);
    }
}
