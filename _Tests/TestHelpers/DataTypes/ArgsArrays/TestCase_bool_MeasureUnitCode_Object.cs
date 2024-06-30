namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_bool_MeasureUnitCode_Object(string TestCase, bool IsTrue, MeasureUnitCode MeasureUnitCode, object Obj) : TestCase_bool_MeasureUnitCode(TestCase, IsTrue, MeasureUnitCode)
{
    public override object[] ToObjectArray() => [TestCase, IsTrue];
}
