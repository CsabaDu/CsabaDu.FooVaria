namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems
{
    public record Enum_Enum_args(Enum MeasureUnit, Enum Context) : Enum_arg(MeasureUnit)
    {
        public override object[] ToObjectArray() => [MeasureUnit, Context];
    }
}
