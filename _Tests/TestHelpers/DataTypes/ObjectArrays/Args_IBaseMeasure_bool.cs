namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record IBaseMeasure_bool_args(string Case, IBaseMeasure BaseMeasure, bool IsTrue) : IBaseMeasure_args(Case, BaseMeasure)
{
    public override object[] ToObjectArray() => [Case, BaseMeasure, IsTrue];
}
