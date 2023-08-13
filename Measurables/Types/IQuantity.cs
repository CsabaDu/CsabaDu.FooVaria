namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IQuantity<out T> : IRound<T>, IExchange<ValueType, decimal> where T : class, IMeasurable
    {
        TypeCode QuantityTypeCode { get; }
        object Quantity { get; init; }

        ValueType GetQuantity();
        ValueType GetQuantity(RoundingMode roundingMode);
        ValueType GetQuantity(TypeCode quantityTypeCode);

        void ValidateQuantity(ValueType quantity, TypeCode? quantityTypeCode = null);
        void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }

}
