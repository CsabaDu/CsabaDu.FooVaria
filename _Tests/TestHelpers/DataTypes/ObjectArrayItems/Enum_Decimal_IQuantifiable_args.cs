namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record Enum_Decimal_IQuantifiable_args(Enum MeasureUnit, decimal DefaultQuantity, IQuantifiable Quantifiable) : Enum_Decimal_args(MeasureUnit, DefaultQuantity)
    {
        public override object[] ToObjectArray() => [MeasureUnit, DefaultQuantity, Quantifiable];
    }
}
