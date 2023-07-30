namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IRateComponentFactory
{
    IRateComponent Create(IBaseMeasure baseMeasure);
    IRateComponent? Create(IRate rate, RateComponentCode? rateComponentCode);

    IRateComponent? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
}
