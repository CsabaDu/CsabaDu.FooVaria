namespace CsabaDu.FooVaria.Measurables.Types;

public interface IQuantity<T> : IRound<T> where T : class, IMeasurable, IRateComponent
{
    object Quantity { get; init; }

    object GetQuantity(RoundingMode roundingMode);
    object GetQuantity(TypeCode quantityTypeCode);
    //decimal GetDecimalQuantity();
    bool TryGetQuantity(ValueType quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity);

    void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode);
}
