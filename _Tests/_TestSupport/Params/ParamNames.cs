namespace CsabaDu.FooVaria.Tests.TestSupport.Params;

public static class ParamNames
{
    internal const string commonBase = nameof(commonBase);
    internal const string factory = nameof(factory);
    internal const string measurable = nameof(measurable);
    internal const string measureUnit = nameof(measureUnit);
    internal const string measureUnitCode = nameof(measureUnitCode);
    internal const string other = nameof(other);
    internal const string paramName = nameof(paramName);
    internal const string rootObject = nameof(rootObject);

    internal static IEnumerable<string> GetParamNames()
    {
        Type type = typeof(ParamNames);
        FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.NonPublic);

        return fields.Select(x => x.Name);
    }
}
