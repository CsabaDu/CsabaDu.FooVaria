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
    public ILimitedRate Create(ILimitedRate other)
    {
        return new LimitedRate(other);
    }

    public ILimitedRate Create(IMeasure numerator, string name, ValueType quantity, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(name, quantity);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, Enum measureUnit, ValueType quantity, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit, quantity);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, string name, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(name);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, Enum measureUnit, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(measureUnit);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ILimit limit)
    {
        return new LimitedRate(this, numerator, measurement, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IMeasurement measurement, ValueType quantity, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(measurement, quantity);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit limit)
    {
        return new LimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IRate rate, ILimit limit)
    {
        IMeasure numerator = NullChecked(rate, nameof(rate)).Numerator;
        IDenominator denominator = rate.Denominator;

        return Create(numerator, denominator, limit);
    }

    public override ILimitedRate Create(IMeasurable other)
    {
        _ = NullChecked(other, nameof(other));

        if (other is ILimitedRate limitedRate) return Create(limitedRate);

        if (other is IRate rate) return Create(rate, CreateLimit(rate.Denominator));

        throw new ArgumentOutOfRangeException(nameof(other), other.GetType(), null);
    }

    public ILimit CreateLimit(IDenominator denominator)
    {
        return (ILimit)LimitFactory.Create(denominator);
    }

    public override ILimitedRate Create(IQuantifiable numerator, MeasureUnitTypeCode measureUnitTypeCode)
    {
        string name = nameof(numerator);

        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);

        ILimit limit = LimitFactory.CreateDefault(measureUnitTypeCode);

        return new LimitedRate(this, measure, measureUnitTypeCode, limit);
    }

    public override IBaseRate Create(IQuantifiable numerator, Enum denominatorMeasureUnit)
    {
        string name = nameof(numerator);

        if (NullChecked(numerator, name) is not IMeasure measure) throw ArgumentTypeOutOfRangeException(name, numerator);
        measure.ValidateMeasureUnit(denominatorMeasureUnit);

        MeasureUnitTypeCode measureUnitTypeCode = MeasureUnitTypes.GetMeasureUnitTypeCode(denominatorMeasureUnit);
        ILimit limit = LimitFactory.CreateDefault(measureUnitTypeCode);

        return new LimitedRate(this, measure, denominatorMeasureUnit, limit);
    }
    #endregion
}
