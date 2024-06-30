namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_IBaseMeasure_bool_IBaseMeasure(string TestCase, IBaseMeasure BaseMeasure, bool IsTrue, IBaseMeasure Other) : TestCase_IBaseMeasure_bool(TestCase, BaseMeasure, IsTrue)
{
    public override object[] ToObjectArray() => [TestCase, BaseMeasure, IsTrue, Other];
}
