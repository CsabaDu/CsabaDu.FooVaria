namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface IRound<out TSelf>
    where TSelf : class, IQuantifiable
{
    TSelf Round(RoundingMode roundingMode);
    object GetQuantity(RoundingMode roundingMode);
}