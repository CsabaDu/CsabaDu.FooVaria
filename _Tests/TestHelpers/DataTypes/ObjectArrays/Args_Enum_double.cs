namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record Args_Enum_double(string Case, Enum MeasureUnit, double Quantity) : Args_Enum(Case, MeasureUnit)
    {
        public override object[] ToObjectArray() => [Case, MeasureUnit, Quantity];
    }
}

