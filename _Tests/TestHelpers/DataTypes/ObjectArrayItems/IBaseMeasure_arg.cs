using CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record IBaseMeasure_arg(IBaseMeasure BaseMeasure) : ObjectArray
    {
        public override object[] ToObjectArray() => [BaseMeasure];
    }
}
