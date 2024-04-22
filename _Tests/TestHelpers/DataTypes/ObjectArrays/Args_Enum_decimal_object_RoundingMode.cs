namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_decimal_object_RoundingMode(string Case, Enum MeasureUnit, decimal DefaultQuantity, object Obj, RoundingMode RoundingMode) : Args_Enum_decimal_object(Case, MeasureUnit, DefaultQuantity, Obj)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, Obj, RoundingMode];
}
