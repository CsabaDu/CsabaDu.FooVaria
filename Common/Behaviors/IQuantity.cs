namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IQuantity
    {

    }

    public interface IQuantity<out TNum> : IQuantity
        where TNum : struct
    {
        TNum GetQuantity();
    }
}
