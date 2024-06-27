using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.Rates.Types;

public interface IFlatRate : IRate, ICalculate<IFlatRate, decimal>, IDeepCopy<IFlatRate>, IConcreteFactory<IFlatRateFactory>
{
    //IFlatRateFactory Factory { get; init; }

    IFlatRate GetFlatRate(IMeasure numerator, string name, ValueType quantity);
    IFlatRate GetFlatRate(IMeasure numerator, string name);
    IFlatRate GetFlatRate(IMeasure numerator, Enum denominatorContext, ValueType quantity);
    IFlatRate GetFlatRate(IMeasure numerator, MeasureUnitCode denominatorCode);
    IFlatRate GetFlatRate(IMeasure numerator, IMeasurement denominator);
    IFlatRate GetFlatRate(IMeasure numerator, IDenominator denominator);
    IFlatRate GetFlatRate(IMeasure numerator);
    //IFlatRate GetFlatRate(IRate baseRate);
}
