namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_MeasureUnitCode_bool_Enum(string TestCase, Enum MeasureUnit, MeasureUnitCode MeasureUnitCode, bool IsTrue, Enum Context) : TestCase_Enum_MeasureUnitCode_bool(TestCase, MeasureUnit, MeasureUnitCode, IsTrue)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, MeasureUnitCode, IsTrue, Context];
    }
}
