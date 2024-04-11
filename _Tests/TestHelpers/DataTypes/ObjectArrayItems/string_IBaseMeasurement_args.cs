namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record string_IBaseMeasurement_args(string ParamName, IBaseMeasurement BaseMeasurement) : string_arg(ParamName)
    {
        public override object[] ToObjectArray() => [ParamName, BaseMeasurement];
    }
}
