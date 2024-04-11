namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record string_arg(string ParamName) : ObjectArray
    {
        public override object[] ToObjectArray() => [ParamName];
    }
}
