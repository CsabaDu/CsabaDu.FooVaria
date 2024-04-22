namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record IBaseMeasure_bool_IBaseMeasure_args(string Case, IBaseMeasure BaseMeasure, bool IsTrue, IBaseMeasure Other) : IBaseMeasure_bool_args(Case, BaseMeasure, IsTrue)
{
    public override object[] ToObjectArray() => [Case, BaseMeasure, IsTrue, Other];
}
