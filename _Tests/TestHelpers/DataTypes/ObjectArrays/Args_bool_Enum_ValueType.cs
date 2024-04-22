namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record Args_bool_Enum_ValueType(string Case, bool IsTrue, Enum MeasureUnit, ValueType Quantity) : Args_bool_Enum(Case, IsTrue, MeasureUnit)
    {
        public override object[] ToObjectArray() => [Case, IsTrue, MeasureUnit, Quantity];
    }
}
