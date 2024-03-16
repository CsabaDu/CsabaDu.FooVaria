namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems;

public record Enum_ILimiter_args(Enum MeasureUnit, ILimiter Limiter) : Enum_arg(MeasureUnit)
{
    public override object[] ToObjectArray() => [MeasureUnit, Limiter];
}
