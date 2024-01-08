namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IBaseMeasureQuantity : IQuantity
{
    object GetQuantity(RoundingMode roundingMode);
    object GetQuantity(TypeCode quantityTypeCode);

    void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramNamme);
}

    //bool TryGetQuantity(ValueType quantity, [NotNullWhen(true)] out ValueType? thisTypeQuantity);