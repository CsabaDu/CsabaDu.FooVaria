namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IQuantifiable_string(string Case, IQuantifiable Quantifiable, string ParamName) : TestCase_IQuantifiable(Case, Quantifiable)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, ParamName];
    }
}