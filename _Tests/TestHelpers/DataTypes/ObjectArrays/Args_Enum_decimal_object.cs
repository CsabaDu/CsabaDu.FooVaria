namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_decimal_object(string Case, Enum MeasureUnit, decimal DefaultQuantity, object Obj) : Args_Enum_decimal(Case, MeasureUnit, DefaultQuantity)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, Obj];
}
