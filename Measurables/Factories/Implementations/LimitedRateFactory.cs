using CsabaDu.FooVaria.Measurables.Types.Implementations;

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
        return CreateLimitedRate(numerator, name, quantity, limit);
    }

    public ILimitedRate Create(IMeasure numerator, Enum measureUnit, ValueType? quantity, ILimit? limit)
    {
        return CreateLimitedRate(numerator, measureUnit, quantity, limit);
    }

    public ILimitedRate Create(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, ILimit? limit)
    {
        return CreateLimitedRate(numerator, measureUnit, exchangeRate, customName, quantity, limit);
    }

    public ILimitedRate Create(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, ILimit? limit)
    {
        return CreateLimitedRate(numerator, customName, measureUnitTypeCode, exchangeRate, quantity, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ValueType? quantity, ILimit? limit)
    {
        return CreateLimitedRate(numerator, measurement, quantity, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit? limit)
    {
        return CreateLimitedRate(numerator, denominator, limit);
    }

    public ILimitedRate Create(IRate rate, ILimit? limit)
    {
        return CreateLimitedRate(this, rate, limit);
    }
    #endregion

    #region Private methods
    private ILimitedRate CreateLimitedRate(IMeasure numerator, string name, ValueType? quantity, ILimit? limit)
    {
        return CreateLimitedRate(numerator, GetMeasurement(name), quantity, limit);
    }

    private ILimitedRate CreateLimitedRate(IMeasure numerator, IMeasurement measurement, ValueType? quantity, ILimit? limit)
    {
        return new LimitedRate(this, numerator, measurement, quantity, limit);
    }

    private ILimitedRate CreateLimitedRate(IMeasure numerator, Enum measureUnit, ValueType? quantity, ILimit? limit)
    {
        return new LimitedRate(this, numerator, measureUnit, quantity, limit);
    }

    private ILimitedRate CreateLimitedRate(IMeasure numerator, IDenominator denominator, ILimit? limit)
    {
        return new LimitedRate(this, numerator, denominator, limit);
    }

    private ILimitedRate CreateLimitedRate(IMeasure numerator, Enum measureUnit, decimal exchangeRate, string customName, ValueType? quantity, ILimit? limit)
    {
        return new LimitedRate(this, numerator, measureUnit, exchangeRate, customName, quantity, limit);
    }

    private ILimitedRate CreateLimitedRate(IMeasure numerator, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate, ValueType? quantity, ILimit? limit)
    {
        return new LimitedRate(this, numerator, customName, measureUnitTypeCode, exchangeRate, quantity, limit);
    }
    #endregion
}
