namespace CsabaDu.FooVaria.Common.Behaviors;

public interface IRound<out TSelf>
    where TSelf : class, IBaseMeasure
{
    TSelf Round(RoundingMode roundingMode);
}