namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors
{
    public interface IQuantity
    {
        object GetQuantity(TypeCode quantityTypeCode);
    }

    public interface IQuantity<out TNum> : IQuantity
        where TNum : struct
    {
        TNum GetQuantity();
    }
}
