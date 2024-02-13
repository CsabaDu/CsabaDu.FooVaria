namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class ParamNames
{
    public const string commonBase = nameof(commonBase);
    public const string factory = nameof(factory);
    public const string measurable = nameof(measurable);
    public const string measureUnit = nameof(measureUnit);
    public const string measureUnitCode = nameof(measureUnitCode);
    public const string other = nameof(other);
    public const string paramName = nameof(paramName);

    public static IEnumerable<string> GetParamNames()
    {
        Type type = typeof(ParamNames);
        FieldInfo[] fields = type.GetFields();

        return fields.Select(x => x.Name);
    }
}
