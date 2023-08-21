namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IQuantity<out T> : IRound<T>, IExchange<ValueType, decimal> where T : class, IMeasurable
    {
        TypeCode QuantityTypeCode { get; }
        object Quantity { get; init; }

        ValueType GetQuantity(ValueType? quantity = null);
        ValueType GetQuantity(RoundingMode roundingMode);
        ValueType GetQuantity(TypeCode quantityTypeCode);
        TypeCode? GetQuantityTypeCode([DisallowNull] ValueType quantity);
        ValueType GetDefaultRateComponentQuantity();

        void ValidateQuantity(ValueType? quantity, TypeCode? quantityTypeCode = null);
        void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }

}
