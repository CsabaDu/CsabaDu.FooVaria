namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record bool_Enum_ValueType_args(bool IsTrue, Enum MeasureUnit, ValueType Quantity) : bool_Enum_args(IsTrue, MeasureUnit)
    {
        public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Quantity];
    }
}
