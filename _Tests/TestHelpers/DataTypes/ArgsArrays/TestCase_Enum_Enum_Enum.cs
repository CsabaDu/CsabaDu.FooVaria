namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_Enum_Enum_Enum(string TestCase, Enum MeasureUnit, Enum Context, Enum OtherMeasureUnit) : TestCase_Enum_Enum(TestCase, MeasureUnit, Context)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, Context, OtherMeasureUnit];
    }
}