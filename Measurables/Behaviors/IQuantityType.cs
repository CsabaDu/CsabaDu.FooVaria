namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IQuantityType
    {
        TypeCode? GetQuantityTypeCode(object quantity);

        void ValidateQuantityTypeCode(TypeCode quantityTypeCode);
    }
}
