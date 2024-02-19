namespace CsabaDu.FooVaria.Rates.Types;

public interface IFlatRate : IRate, ICalculate<IFlatRate, decimal>, ICommonBase<IFlatRate>
{
    IFlatRate GetFlatRate(IMeasure numerator, string name, ValueType quantity);
    IFlatRate GetFlatRate(IMeasure numerator, string name);
    IFlatRate GetFlatRate(IMeasure numerator, Enum denominatorMeasureUnit, ValueType quantity);
    IFlatRate GetFlatRate(IMeasure numerator, MeasureUnitCode denominatorCode);
    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement denominatorMeasurement);
    IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator);
    IFlatRate GetFlatRate(IMeasure numerator);
    IFlatRate GetFlatRate(IBaseRate baseRate);
}
