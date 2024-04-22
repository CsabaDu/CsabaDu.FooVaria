namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_decimal(string Case, Enum MeasureUnit, decimal DefaultQuantity) : Args_Enum(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity];
}
