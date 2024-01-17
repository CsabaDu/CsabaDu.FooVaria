namespace CsabaDu.FooVaria.BaseMeasures.Behaviors;

public interface IRound<out TSelf>
    where TSelf : class, IBaseMeasure
{
    TSelf Round(RoundingMode roundingMode);
    object GetQuantity(RoundingMode roundingMode);
}