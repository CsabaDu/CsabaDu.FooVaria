namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record bool_arg(bool IsTrue) : ObjectArray
    {
        public override object[] ToObjectArray() => [IsTrue];
    }

    public record string_arg(string ParamName) : ObjectArray
    {
        public override object[] ToObjectArray() => [ParamName];
    }

    public record string_IBaseMeasurement_args(string ParamName, IBaseMeasurement BaseMeasurement) : string_arg(ParamName)
    {
        public override object[] ToObjectArray() => [ParamName, BaseMeasurement];
    }
}
