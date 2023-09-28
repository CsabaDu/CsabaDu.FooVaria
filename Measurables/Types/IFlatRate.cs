namespace CsabaDu.FooVaria.Measurables.Types;

public interface IFlatRate : IRate, ICalculable, ICalculate<decimal, IFlatRate>, IMultiply<IMeasure, IMeasure>
{
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName);

    IFlatRate GetFlatRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);

    IFlatRate GetFlatRate(IMeasure numerator, string customName, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, string customName);

    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, Enum measureUnit);

    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement, decimal quantity);
    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement measurement);

    IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator);
    IFlatRate GetFlatRate(IMeasure numerator);

    IFlatRate GetFlatRate(IRate rate);

    IFlatRate GetFlatRate(IFlatRate other);
    //IFlatRateFactory GetFactory();
}
