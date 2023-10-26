namespace CsabaDu.FooVaria.Measurables.Types
{
    public interface IQuantityType
    {
        TypeCode QuantityTypeCode { get; }

        TypeCode? GetQuantityTypeCode(object quantity);

        void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }
}
