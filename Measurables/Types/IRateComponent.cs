namespace CsabaDu.FooVaria.Measurables.Types;

public interface IRateComponent
{
    IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
}
