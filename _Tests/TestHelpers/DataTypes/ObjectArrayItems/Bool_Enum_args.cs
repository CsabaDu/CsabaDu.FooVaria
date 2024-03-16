namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Bool_Enum_args(bool IsTrue, Enum MeasureUnit) : Bool_arg(IsTrue)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnit];
}
