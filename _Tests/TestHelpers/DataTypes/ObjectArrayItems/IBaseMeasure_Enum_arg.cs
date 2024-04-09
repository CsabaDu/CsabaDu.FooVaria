namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record IBaseMeasure_Enum_arg(IBaseMeasure BaseMeasure, Enum MeasureUnit) : IBaseMeasure_arg(BaseMeasure)
    {
        public override object[] ToObjectArray() => [BaseMeasure, MeasureUnit];
    }
}
