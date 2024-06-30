namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays
{
    public record TestCase_string_IBaseMeasurement(string TestCase, string ParamName, IBaseMeasurement BaseMeasurement) : TestCase_string(TestCase, ParamName)
    {
        public override object[] ToObjectArray() => [TestCase, ParamName, BaseMeasurement];
    }
}
