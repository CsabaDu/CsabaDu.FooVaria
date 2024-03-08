namespace CsabaDu.FooVaria.Rates.Factories.Implementations;

public sealed class LimitedRateFactory(IDenominatorFactory denominatorFactory, ILimitFactory limitFactory) : RateFactory(denominatorFactory), ILimitedRateFactory
{
    #region Properties
    public ILimitFactory LimitFactory { get; init; } = NullChecked(limitFactory, nameof(limitFactory));
    #endregion

    #region Public methods
    public ILimitedRate Create(IMeasure numerator, string name, ValueType denominatorQuantity, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(name, denominatorQuantity);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, string name, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(name);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, Enum denominatorContext, ValueType denominatorQuantity, ILimit limit)
    {
        MeasureUnitElements denominatorElements = GetMeasureUnitElements(denominatorContext, nameof(denominatorContext));
        Enum measureUnit = denominatorElements.MeasureUnit;
        IDenominator denominator = DenominatorFactory.Create(measureUnit);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, MeasureUnitCode denominatorCode, ILimit limit)
    {
        IDenominator denominator = DenominatorFactory.Create(denominatorCode);

        return Create(numerator, denominator, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IMeasurement denominator, ILimit limit)
    {
        IDenominator baseMeasure = DenominatorFactory.Create(denominator);

        return new LimitedRate(this, numerator, baseMeasure, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IDenominator denominator, ILimit limit)
    {
        return new LimitedRate(this, numerator, denominator, limit);
    }

    public ILimitedRate Create(IRate rate, ILimit limit)
    {
        return new LimitedRate(this, rate, limit);
    }

    public ILimitedRate CreateNew(ILimitedRate other)
    {
        return new LimitedRate(other);
    }

    public ILimit CreateLimit(IBaseMeasure baseMeasure)
    {
        return LimitFactory.Create(baseMeasure, default);
    }

    #region Override methods
    public override ILimitedRate Create(params IBaseMeasure[] rateComponents)
    {
        const string paramName = nameof(rateComponents);
        int count = rateComponents?.Length ?? 0;

        if (count < 2 || count > 3) throw QuantityArgumentOutOfRangeException(paramName, count);

        IMeasure numerator = GetValidNumerator(rateComponents![0], paramName);
        IBaseMeasure baseMeasure = rateComponents[1];

        if (baseMeasure is not IDenominator denominator)
        {
            denominator = CreateDenominator(baseMeasure);
        }

        if (count == 3)
        {
            baseMeasure = rateComponents[2];
        }
        else
        {
            baseMeasure = CreateLimit(baseMeasure);
        }

        if (baseMeasure is not ILimit limit)
        {
            limit = CreateLimit(baseMeasure);
        }

        return Create(numerator, denominator, limit);
    }

    public override ILimitedRate CreateBaseRate(IQuantifiable numerator, Enum denominatorContext)
    {
        IMeasure measure = GetValidNumerator(numerator, nameof(numerator));
        IDenominator denominator = DenominatorFactory.Create(denominatorContext);
        ILimit limit = CreateLimit(denominator);

        return Create(measure, denominator, limit);
    }

    public override ILimitedRate CreateNew(IRate other)
    {
        if (other is ILimitedRate limitedRate) return CreateNew(limitedRate);

        IDenominator denominator = NullChecked(other, nameof(other)).Denominator;
        ILimit limit = CreateLimit(denominator);

        return Create(other, limit);
    }
    #endregion
    #endregion
}
