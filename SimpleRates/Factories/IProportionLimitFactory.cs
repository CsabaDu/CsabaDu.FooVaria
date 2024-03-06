namespace CsabaDu.FooVaria.ProportionLimits.Factories;

public interface IProportionLimitFactory : ISimpleRateFactory
{
    IProportionLimit Create(IBaseRate baseRate, LimitMode limitMode);
    IProportionLimit Create(IQuantifiable numerator, IQuantifiable denominator, LimitMode limitMode);
    IProportionLimit Create(IQuantifiable numerator, IBaseMeasurement denominator, LimitMode limitMode);
    IProportionLimit Create(Enum numeratorContext, decimal quantity, Enum denominator, LimitMode limitMode);
    IProportionLimit Create(IQuantifiable numerator, Enum denominator, LimitMode limitMode);
}
