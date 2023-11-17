namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IRound<out T> where T : class, IBaseMeasureTemp
{
    T Round(RoundingMode roundingMode);
}