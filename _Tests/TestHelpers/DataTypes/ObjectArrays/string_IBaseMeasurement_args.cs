namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record string_IBaseMeasurement_args(string Case, string ParamName, IBaseMeasurement BaseMeasurement) : string_args(Case, ParamName)
    {
        public override object[] ToObjectArray() => [Case, ParamName, BaseMeasurement];
    }
}
