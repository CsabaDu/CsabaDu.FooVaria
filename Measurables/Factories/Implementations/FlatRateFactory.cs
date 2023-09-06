using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class FlatRateFactory : RateFactory, IFlatRateFactory
{
    #region Constructors
    public FlatRateFactory(IDenominatorFactory denominatorFactory) : base(denominatorFactory)
    {
    }
    #endregion

    #region Public methods
    public IFlatRate Create(IFlatRate flatRate)
    {
        return CreateFlatRate(flatRate);
    }

    public IFlatRate Create(IMeasure numerator, string name, ValueType? quantity)
    {
        return CreateFlatRate(numerator, name, quantity);
    }

    public IFlatRate Create(IMeasure numerator, Enum measureUnit, ValueType? quantity)
    {
        return CreateFlatRate(numerator, measureUnit, quantity);
    }

    public IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity)
    {
        return CreateFlatRate(numerator, measureUnit, exchangeRate, customName, quantity);
    }

    public IFlatRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity)
    {
        return CreateFlatRate(numerator, customName, measureUnitTypeCode, exchangeRate, quantity);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity)
    {
        return CreateFlatRate(numerator, measurement, quantity);
    }

    public IFlatRate Create(IMeasure numerator, IDenominator denominator)
    {
        return CreateFlatRate(numerator, denominator);
    }

    public IFlatRate Create(IRate rate)
    {
        return CreateFlatRate(this, rate);
    }
    #endregion

    #region Private methods
    private IFlatRate CreateFlatRate(IMeasure numerator, string name, ValueType? quantity)
    {
        return CreateFlatRate(numerator, GetMeasurement(name), quantity);
    }

    private IFlatRate CreateFlatRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity)
    {
        return new FlatRate(this, numerator, measurement, quantity);
    }

    private IFlatRate CreateFlatRate(IMeasure numerator, Enum measureUnit, ValueType? quantity)
    {
        return new FlatRate(this, numerator, measureUnit, quantity);
    }

    private IFlatRate CreateFlatRate(IMeasure numerator, IDenominator denominator)
    {
        return new FlatRate(this, numerator, denominator);
    }

    private IFlatRate CreateFlatRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity)
    {
        return new FlatRate(this, numerator, measureUnit, exchangeRate, customName, quantity);
    }

    private IFlatRate CreateFlatRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity)
    {
        return new FlatRate(this, numerator, customName, measureUnitTypeCode, exchangeRate, quantity);
    }
    #endregion
}
