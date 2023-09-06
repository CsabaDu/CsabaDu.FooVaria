namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IFlatRateFactory : IRateFactory
{
    IFlatRate Create(IFlatRate flatRate);
    IFlatRate Create(IMeasure numerator, string customName, decimal? quantity);
    IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal? quantity);
    IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, decimal? quantity);
    IFlatRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, decimal? quantity);
    IFlatRate Create(IMeasure numerator, IMeasurement measurement, decimal? quantity);
    IFlatRate Create(IMeasure numerator, IDenominator denominator);
    IFlatRate Create(IRate rate);
}
