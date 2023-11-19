namespace CsabaDu.FooVaria.Rates.Types;

public interface IFlatRate : IRate<IFlatRate>, ICalculate, ICalculate<decimal, IFlatRate>, ICommonBase<IFlatRate>
{
    IFlatRate GetFlatRate(IMeasure numerator, string name, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, string name);
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit);
    IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator);
    IFlatRate GetFlatRate(IMeasure numerator);
}

    //IFlatRate GetFlatRate(IRate rate);
    //IFlatRate GetFlatRate(IFlatRate other);
    //IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, decimal quantity);
    //IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement);