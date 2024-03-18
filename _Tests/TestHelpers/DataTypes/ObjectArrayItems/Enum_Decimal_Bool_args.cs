namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_Decimal_Bool_args(Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue) : Enum_Decimal_args(MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, IsTrue];
}
