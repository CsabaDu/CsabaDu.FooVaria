namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IRound<out T> where T : class, IBaseMeasurable, IQuantifiable
{
    T Round(RoundingMode roundingMode);
}