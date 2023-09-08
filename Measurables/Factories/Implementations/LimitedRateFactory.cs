namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class LimitedRateFactory : RateFactory, ILimitedRateFactory
{
    #region Constructors
    public LimitedRateFactory(IDenominatorFactory denominatorFactory, ILimitFactory limitFactory) : base(denominatorFactory)
    {
        LimitFactory = NullChecked(limitFactory, nameof(limitFactory));
    }
    #endregion

    #region Properties
    public ILimitFactory LimitFactory { get; init; }
    #endregion

    #region Public methods
    public ILimitedRate Create(ILimitedRate limitedRate, ILimit? limit)
    {
        return CreateLimitedRate(limitedRate, limit);
    }

    public ILimitedRate Create(IMeasure numerator, string name, ValueType? quantity, ILimit? limit)
    {
        IDenominator denominator = DenominatorFactory.Create(name, quantity);

        return CreateLimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, Enum measureUnit, ValueType? quantity, ILimit? limit)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit, quantity);

        return CreateLimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, ILimit? limit)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit, exchangeRate, customName, quantity);

        return CreateLimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, ILimit? limit)
    {
        IDenominator denominator = DenominatorFactory.Create(customName, measureUnitTypeCode, exchangeRate, quantity);

        return CreateLimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity, ILimit? limit)
    {
        IDenominator denominator = DenominatorFactory.Create(measurement, quantity);

        return CreateLimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit? limit)
    {
        return CreateLimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IRate rate, ILimit? limit)
    {
        return CreateLimitedRate(this, rate, limit);
    }
    #endregion
}
