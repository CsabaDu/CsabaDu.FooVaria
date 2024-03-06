namespace CsabaDu.FooVaria.Rates.Factories;

public interface IFlatRateFactory : IRateFactory, IFactory<IFlatRate>
{
    IFlatRate Create(IMeasure numerator, string name, ValueType denominatorQuantity);
    IFlatRate Create(IMeasure numerator, string name);
    IFlatRate Create(IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity);
    IFlatRate Create(IMeasure numerator, MeasureUnitCode denominatorCode);
    IFlatRate Create(IMeasure numerator, IMeasurement denominatorMeasurement);
    IFlatRate Create(IMeasure numerator, IDenominator denominator);
    //IFlatRate Create(IRate rate);
}
