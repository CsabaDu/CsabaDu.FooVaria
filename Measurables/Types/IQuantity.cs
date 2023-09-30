namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IQuantity<T> : IRound<T>, IExchange<ValueType, decimal> where T : class, IMeasurable, IRateComponent, IExchangeable<T, Enum>
    {
        TypeCode QuantityTypeCode { get; }
        object Quantity { get; init; }

        ValueType GetQuantity();
        ValueType GetQuantity(RoundingMode roundingMode);
        ValueType GetQuantity(TypeCode quantityTypeCode);
        TypeCode? GetQuantityTypeCode(ValueType quantity);
        decimal GetDecimalQuantity(/*T? other = null*/);
        bool TryGetQuantity(ValueType? quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity);

        void ValidateQuantity(ValueType? quantity);
        void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode);
        void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }

}
