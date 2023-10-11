namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IQuantifyable<T> where T : struct
{
    T GetQuantity();
    decimal GetDefaultQuantity();

    void ValidateQuantity(T quantity);
}
