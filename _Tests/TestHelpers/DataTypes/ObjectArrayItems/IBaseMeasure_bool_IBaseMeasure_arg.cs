namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record IBaseMeasure_bool_IBaseMeasure_arg(IBaseMeasure BaseMeasure, bool IsTrue, IBaseMeasure Other) : IBaseMeasure_bool_arg(BaseMeasure, IsTrue)
{
    public override object[] ToObjectArray() => [BaseMeasure, IsTrue, Other];
}
