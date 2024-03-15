namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems;

public record Enum_arg(Enum MeasureUnit) : ObjectArray
{
    public override object[] ToObjectArray() => [MeasureUnit];
}
