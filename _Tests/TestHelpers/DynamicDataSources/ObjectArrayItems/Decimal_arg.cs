namespace CsabaDu.FooVaria.Tests.TestHelpers.DynamicDataSources.ObjectArrayItems
{
    public record Decimal_arg(decimal ExchangeRate) : ObjectArray
    {
        public override object[] ToObjectArray() => [ExchangeRate];
    }
}
