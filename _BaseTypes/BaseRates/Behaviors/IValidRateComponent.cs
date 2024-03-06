namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Behaviors;

public interface IValidRateComponent
{
    object? GetRateComponent(RateComponentCode rateComponentCode);
    bool IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode);
}
