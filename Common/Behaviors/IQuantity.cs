namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IQuantity<out T> /*: IQuantifiable*/ where T : struct
{
    T GetQuantity();
}
