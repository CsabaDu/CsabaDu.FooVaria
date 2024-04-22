namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record Args_string_IBaseMeasurement(string Case, string ParamName, IBaseMeasurement BaseMeasurement) : Args_string(Case, ParamName)
    {
        public override object[] ToObjectArray() => [Case, ParamName, BaseMeasurement];
    }
}
