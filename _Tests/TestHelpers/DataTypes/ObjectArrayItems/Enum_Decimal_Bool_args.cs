namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_Decimal_Bool_args(Enum MeasureUnit, decimal ExchangeRate, bool IsTrue) : Enum_Decimal_args(MeasureUnit, ExchangeRate)
{
    public override object[] ToObjectArray() => [MeasureUnit, ExchangeRate, IsTrue];
}
