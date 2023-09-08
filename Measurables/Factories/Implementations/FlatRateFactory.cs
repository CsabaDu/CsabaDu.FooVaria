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
        IDenominator denominator = DenominatorFactory.Create(name, quantity);

        return CreateFlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, Enum measureUnit, ValueType? quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit, quantity);

        return CreateFlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit, exchangeRate, customName, quantity);

        return CreateFlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(customName, measureUnitTypeCode, exchangeRate, quantity);

        return CreateFlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity)
    {
        IDenominator denominator = DenominatorFactory.Create(measurement, quantity);

        return CreateFlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, IDenominator denominator)
    {
        return CreateFlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IRate rate)
    {
        return CreateFlatRate(this, rate);
    }
    #endregion
}
