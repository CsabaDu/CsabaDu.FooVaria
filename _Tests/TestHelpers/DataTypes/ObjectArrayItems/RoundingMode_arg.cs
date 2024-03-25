namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.ObjectArrayItems
{
    public record RoundingMode_arg(RoundingMode RoundingMode) : ObjectArray
    {
        public override object[] ToObjectArray() => [RoundingMode];
    }
}
