namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IQuantifiable_Enum_string(string Case, IQuantifiable Quantifiable, Enum MeasureUnit, string ParamName) : TestCase_IQuantifiable_Enum(Case, Quantifiable, MeasureUnit)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, MeasureUnit, ParamName];
    }
}