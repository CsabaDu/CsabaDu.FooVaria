namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record Enum_decimal_IQuantifiable_args(Enum MeasureUnit, decimal DefaultQuantity, IQuantifiable Quantifiable) : Enum_decimal_args(MeasureUnit, DefaultQuantity)
    {
        public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, Quantifiable];
    }
}
