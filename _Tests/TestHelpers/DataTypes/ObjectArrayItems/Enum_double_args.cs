namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record Enum_double_args(Enum MeasureUnit, double Quantity) : Enum_arg(MeasureUnit)
    {
        public override object[] ToObjectArray() => [MeasureUnit, Quantity];
    }
}

