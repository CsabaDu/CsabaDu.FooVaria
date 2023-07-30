namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IQuantity<out T> : IRound<T>, IExchange<ValueType, decimal> where T : class, IBaseMeasure
    {
        TypeCode QuantityTypeCode { get; init; }
        decimal DecimalQuantity { get; init; }

        ValueType GetQuantity();
        ValueType GetQuantity(RoundingMode roundingMode);
        ValueType GetQuantity(TypeCode quantityTypeCode);

        void ValidateQuantity(ValueType quantity, TypeCode quantityTypeCode = TypeCode.Object);
        void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }

}
