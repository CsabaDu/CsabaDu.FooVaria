namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IRound<out T> where T : class
{
    T Round(RoundingMode roundingMode);
}