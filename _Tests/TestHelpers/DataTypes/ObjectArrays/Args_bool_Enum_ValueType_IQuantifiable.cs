namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_bool_Enum_ValueType_IQuantifiable(string Case, bool IsTrue, Enum MeasureUnit, ValueType Quantity, IQuantifiable Quantifiable) : Args_bool_Enum_ValueType(Case, IsTrue, MeasureUnit, Quantity)
{
    public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnit, Quantity, Quantifiable];
}
