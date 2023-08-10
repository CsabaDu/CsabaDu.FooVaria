namespace CsabaDu.FooVaria.Measurables.Markers;

public interface IRateComponent
{
    IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
}
