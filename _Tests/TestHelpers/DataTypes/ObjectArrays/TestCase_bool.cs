namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record TestCase_bool(string Case, bool IsTrue) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [TestCase, IsTrue];
    }

    public record TestCase_IQuantifiable_Enum(string Case, IQuantifiable Quantifiable, Enum MeasureUnit) : TestCase_IQuantifiable(Case, Quantifiable)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, MeasureUnit];
    }

    public record TestCase_IQuantifiable_IMeasurable(string Case, IQuantifiable Quantifiable, IMeasurable Measurable) : TestCase_IQuantifiable(Case, Quantifiable)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, Measurable];
    }

    public record TestCase_IQuantifiable_IQuantifiable(string Case, IQuantifiable Quantifiable, IQuantifiable Other) : TestCase_IQuantifiable(Case, Quantifiable)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, Other];
    }

    public record TestCase_IQuantifiable_Enum_string(string Case, IQuantifiable Quantifiable, Enum MeasureUnit, string ParamName) : TestCase_IQuantifiable_Enum(Case, Quantifiable, MeasureUnit)
    {
        public override object[] ToObjectArray() => [TestCase, Quantifiable, MeasureUnit, ParamName];
    }
}