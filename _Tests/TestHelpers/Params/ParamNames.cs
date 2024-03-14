namespace CsabaDu.FooVaria.Tests.TestHelpers.Params;

public static class ParamNames
{
    public const string commonBase = nameof(commonBase);
    public const string context = nameof(context);
    public const string factory = nameof(factory);
    public const string measurable = nameof(measurable);
    public const string measureUnit = nameof(measureUnit);
    public const string measureUnitCode = nameof(measureUnitCode);
    public const string other = nameof(other);
    public const string paramName = nameof(paramName);
    public const string rootObject = nameof(rootObject);    public static IEnumerable<string> GetParamNames()
    {
        Type type = typeof(ParamNames);
        FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);

        return fields.Select(x => x.Name);
    }
}
