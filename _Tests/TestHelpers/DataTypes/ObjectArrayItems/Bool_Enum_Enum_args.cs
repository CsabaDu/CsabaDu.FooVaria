namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Bool_Enum_Enum_args(bool IsTrue, Enum MeasureUnit, Enum Context) : Bool_Enum_args(IsTrue, MeasureUnit)
{
    public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Context];
}
