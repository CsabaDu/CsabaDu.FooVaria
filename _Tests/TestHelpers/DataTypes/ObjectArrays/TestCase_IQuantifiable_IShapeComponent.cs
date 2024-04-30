namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record TestCase_IQuantifiable_IShapeComponent(string Case, IQuantifiable Quantifiable, IShapeComponent ShapeComponent) : TestCase_IQuantifiable(Case, Quantifiable)
{
    public override object[] ToObjectArray() => [TestCase, Quantifiable, ShapeComponent];
}
