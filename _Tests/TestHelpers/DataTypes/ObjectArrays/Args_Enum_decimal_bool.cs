namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_decimal_bool(string Case, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue) : Args_Enum_decimal(Case, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, IsTrue];
}
