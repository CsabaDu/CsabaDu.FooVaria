namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record IBaseMeasure_bool_arg (IBaseMeasure BaseMeasure, bool IsTrue) : IBaseMeasure_arg(BaseMeasure)
{
    public override object[] ToObjectArray() => [BaseMeasure, IsTrue];
}
