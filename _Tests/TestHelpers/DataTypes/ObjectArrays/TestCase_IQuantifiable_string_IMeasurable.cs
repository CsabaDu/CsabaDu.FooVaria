namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IQuantifiable_string_IMeasurable(string Case, IQuantifiable Quantifiable, string ParamName, IMeasurable Measurable) : TestCase_IQuantifiable_string(Case, Quantifiable, ParamName)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, ParamName, Measurable];
    }
}