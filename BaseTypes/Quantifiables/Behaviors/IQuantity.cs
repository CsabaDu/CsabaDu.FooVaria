using CsabaDu.FooVaria.Quantifiables.Enums;

namespace CsabaDu.FooVaria.Quantifiables.Behaviors
{
    public interface IQuantity
    {
        object GetQuantity(RoundingMode roundingMode);
        object GetQuantity(TypeCode quantityTypeCode);

        void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramNamme);
    }

    public interface IQuantity<out TNum> : IQuantity
        where TNum : struct
    {
        decimal DefaultQuantity { get; init; }

        TNum GetQuantity();
    }
}
