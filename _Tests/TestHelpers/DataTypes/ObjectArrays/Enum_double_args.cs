namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record Enum_double_args(string Case, Enum MeasureUnit, double Quantity) : Enum_args(Case, MeasureUnit)
    {
        public override object[] ToObjectArray() => [Case, MeasureUnit, Quantity];
    }
}

