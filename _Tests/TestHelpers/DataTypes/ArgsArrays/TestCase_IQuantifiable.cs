namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ArgsArrays;

public record TestCase_IQuantifiable(string Case, IQuantifiable Quantifiable) : ArgsArray(Case)
{
    public override object[] ToObjectArray() => [TestCase, Quantifiable];
}
