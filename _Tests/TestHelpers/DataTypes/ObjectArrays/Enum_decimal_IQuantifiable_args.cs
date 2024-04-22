namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record Enum_decimal_IQuantifiable_args(string Case, Enum MeasureUnit, decimal DefaultQuantity, IQuantifiable Quantifiable) : Enum_decimal_args(Case, MeasureUnit, DefaultQuantity)
    {
        public override object[] ToObjectArray() => [Case, MeasureUnit, DefaultQuantity, Quantifiable];
    }
}
