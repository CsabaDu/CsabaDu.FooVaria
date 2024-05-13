namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IQuantifiable_string_IQuantifiable(string Case, IQuantifiable Quantifiable, string ParamName, IQuantifiable Other) : TestCase_IQuantifiable_string(Case, Quantifiable, ParamName)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, ParamName, Other];
    }
}