namespace CsabaDu.FooVaria.Measurables.Types;

public interface IQuantity<T> : IRound<T> where T : class, IMeasurable, IRateComponent
{
    TypeCode QuantityTypeCode { get; }
    object Quantity { get; init; }

    object GetQuantity(RoundingMode roundingMode);
    object GetQuantity(TypeCode quantityTypeCode);
    TypeCode? GetQuantityTypeCode(object quantity);
    decimal GetDecimalQuantity();
    bool TryGetQuantity(ValueType quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity);

    void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode);
    void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
}
