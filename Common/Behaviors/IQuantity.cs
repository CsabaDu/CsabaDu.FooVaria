namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IQuantity<out TNum> where TNum : struct
{
    TNum GetQuantity();
}
