namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_IQuantifiable(string Case, IQuantifiable Quantifiable) : ObjectArray(Case)
{
    public override object[] ToObjectArray() => [TestCase, Quantifiable];
}
