namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IQuantity<T> : IRound<T>, IExchange<ValueType, decimal> where T : class, IMeasurable, IRateComponent, IExchangeable<T, Enum>
    {
        TypeCode QuantityTypeCode { get; }
        object Quantity { get; init; }

        ValueType GetQuantity(ValueType? quantity = null);
        ValueType GetQuantity(RoundingMode roundingMode);
        ValueType GetQuantity(TypeCode quantityTypeCode);
        TypeCode? GetQuantityTypeCode([DisallowNull] ValueType quantity);
        ValueType GetDefaultRateComponentQuantity();
        decimal GetDecimalQuantity(T? other = null);
        bool TryGetQuantity(ValueType? quantity, [NotNullWhen(true)] out ValueType? rateComponentQuantity);

        void ValidateQuantity(ValueType? quantity, TypeCode? quantityTypeCode = null);
        void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }

}
