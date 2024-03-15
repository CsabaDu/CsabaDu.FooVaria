namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.ObjectArrayItems;

public record Enum_arg(Enum MeasureUnit) : ObjectArray
{
    public override object[] ToObjectArray() => [MeasureUnit];
}
