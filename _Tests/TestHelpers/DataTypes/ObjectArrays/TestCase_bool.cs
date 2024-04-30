namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool(string Case, bool IsTrue) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue];
    }

    public record TestCase_IQuantifiable(string Case, IQuantifiable Quantifiable) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable];
    }

    public record TestCase_IQuantifiable_IShapeComponent(string Case, IQuantifiable Quantifiable, IShapeComponent ShapeComponent) : TestCase_IQuantifiable(Case, Quantifiable)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, ShapeComponent];
    }
}
