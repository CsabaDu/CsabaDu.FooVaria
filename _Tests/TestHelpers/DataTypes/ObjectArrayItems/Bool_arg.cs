namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record bool_arg(bool IsTrue) : ObjectArray
    {
        public override object[] ToObjectArray() => [IsTrue];
    }
}
