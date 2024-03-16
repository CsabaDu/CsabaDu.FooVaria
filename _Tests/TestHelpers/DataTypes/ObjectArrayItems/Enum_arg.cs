namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_arg(Enum MeasureUnit) : ObjectArray
{
    public override object[] ToObjectArray() => [MeasureUnit];
}
