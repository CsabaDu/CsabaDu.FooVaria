namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record bool_Enum_ValueType_IQuantifiable_args(bool IsTrue, Enum MeasureUnit, ValueType Quantity, IQuantifiable Quantifiable) : bool_Enum_ValueType_args(IsTrue, MeasureUnit, Quantity)
    {
        public override object[] ToObjectArray() => [IsTrue, MeasureUnit, Quantity, Quantifiable];
    }
}
