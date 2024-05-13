namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_IQuantifiable_Enum(string Case, IQuantifiable Quantifiable, Enum MeasureUnit) : TestCase_IQuantifiable(Case, Quantifiable)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, MeasureUnit];
    }
}