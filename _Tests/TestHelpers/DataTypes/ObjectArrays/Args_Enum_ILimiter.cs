namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays;

public record Args_Enum_ILimiter(string Case, Enum MeasureUnit, ILimiter Limiter) : Args_Enum(Case, MeasureUnit)
{
    public override object[] ToObjectArray() => [Case, MeasureUnit, Limiter];
}
