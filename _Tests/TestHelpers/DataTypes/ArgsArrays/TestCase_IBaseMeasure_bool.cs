namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_IBaseMeasure_bool(string TestCase, IBaseMeasure BaseMeasure, bool IsTrue) : TestCase_IBaseMeasure(TestCase, BaseMeasure)
{
    public override object[] ToObjectArray() => [TestCase, BaseMeasure, IsTrue];
}
