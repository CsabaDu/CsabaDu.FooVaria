namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_double(string TestCase, Enum MeasureUnit, double Quantity) : TestCase_Enum(TestCase, MeasureUnit)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, Quantity];
    }
}

