namespace CsabaDu.FooVaria.RateComponents.Types;

public interface IFlatRate : IRate, ICalculate, ICalculate<decimal, IFlatRate>
{
    IFlatRate GetFlatRate(IMeasure numerator, string name, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, string name);
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit);
    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement);
    IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator);
    IFlatRate GetFlatRate(IMeasure numerator);
    IFlatRate GetFlatRate(IRate rate);
    IFlatRate GetFlatRate(IFlatRate other);
}
