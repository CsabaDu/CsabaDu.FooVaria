namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrays
{
    public record bool_args(string Case, bool IsTrue) : ObjectArray(Case)
    {
        public override object[] ToObjectArray() => [Case, IsTrue];
    }
}
