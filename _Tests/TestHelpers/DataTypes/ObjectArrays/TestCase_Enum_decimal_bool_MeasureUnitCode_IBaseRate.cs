using CsabaDu.FooVaria.BaseTypes.BaseRates.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_Enum_decimal_bool_MeasureUnitCode_IBaseRate(string TestCase, Enum MeasureUnit, decimal DefaultQuantity, bool IsTrue, MeasureUnitCode MeasureUnitCode, IBaseRate BaseRate) : TestCase_Enum_decimal_bool_MeasureUnitCode(TestCase, MeasureUnit, DefaultQuantity, IsTrue, MeasureUnitCode)
    {
        public override object[] ToObjectArray() => [TestCase, MeasureUnit, DefaultQuantity, IsTrue, MeasureUnitCode, BaseRate];
    }
}
