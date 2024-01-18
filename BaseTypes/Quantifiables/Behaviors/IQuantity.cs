namespace CsabaDu.FooVaria.Quantifiables.Behaviors
{
    public interface IQuantity
    {
        object GetQuantity(TypeCode quantityTypeCode);

        void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName);
    }

    public interface IQuantity<out TNum> : IQuantity
        where TNum : struct
    {
        TNum GetQuantity();
    }
}
