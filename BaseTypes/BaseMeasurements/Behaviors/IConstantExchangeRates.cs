namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Behaviors;

public interface IConstantExchangeRates
{
    IDictionary<object, decimal> GetConstantExchangeRateCollection();
    IDictionary<object, decimal> GetConstantExchangeRateCollection(MeasureUnitCode measureUnitCode);

    //void RestoreConstantExchangeRates();
}
