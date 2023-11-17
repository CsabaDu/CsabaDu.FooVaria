namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IFlatRateFactory : IRateFactory
{
    IFlatRate Create(IFlatRate flatRate);
    IFlatRate Create(IMeasure numerator, string name, ValueType quantity);
    IFlatRate Create(IMeasure numerator, Enum measureUnit, ValueType quantity);
    IFlatRate Create(IMeasure numerator, string name);
    IFlatRate Create(IMeasure numerator, Enum measureUnit);
    IFlatRate Create(IMeasure numerator, IMeasurement measurement);
    IFlatRate Create(IMeasure numerator, IMeasurement measurement, ValueType quantity);
    IFlatRate Create(IMeasure numerator, IDenominator denominator);
    //IFlatRate Create(IRate rate);
}

    //IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType quantity);
    //IFlatRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType quantity);
