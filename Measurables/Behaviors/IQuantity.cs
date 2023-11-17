namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IQuantity
{
    object GetQuantity(RoundingMode roundingMode);
    object GetQuantity(TypeCode quantityTypeCode);
    bool TryGetQuantity(ValueType quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity);

    void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramNamme);
}
