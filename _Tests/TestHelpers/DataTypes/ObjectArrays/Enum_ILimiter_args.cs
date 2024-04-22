namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Enum_ILimiter_args(string Case, Enum MeasureUnit, ILimiter Limiter) : Enum_args(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Limiter];
}
