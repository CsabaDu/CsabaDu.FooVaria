namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record IBaseMeasure_Enum_args(string Case, IBaseMeasure BaseMeasure, Enum MeasureUnit) : IBaseMeasure_args(Case, BaseMeasure)
    {
        public override object[] ToObjectArray() => [Case, BaseMeasure, MeasureUnit];
    }
}
