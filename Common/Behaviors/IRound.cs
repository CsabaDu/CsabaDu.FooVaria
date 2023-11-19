namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IRound<out T> where T : class, IBaseMeasure
{
    T Round(RoundingMode roundingMode);
}