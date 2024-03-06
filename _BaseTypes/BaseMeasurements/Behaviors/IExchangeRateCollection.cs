namespace CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Behaviors;

public interface IExchangeRateCollection : IConstantExchangeRateCollection, IExchangeRate
{
    IDictionary<object, decimal> GetExchangeRateCollection();
}
