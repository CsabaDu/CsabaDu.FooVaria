namespace CsabaDu.FooVaria.Rates.Factories.Implementations;

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

    public ILimitedRate Create(IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity, ILimit limit)
    {
        return new LimitedRate(this, numerator, denominatorMeasureUnit, denominatorQuantity, limit);
    }

    public ILimitedRate Create(IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode, ILimit limit)
    {
        return new LimitedRate(this, numerator, denominatorMeasureUnitCode, limit);
    }

    public ILimitedRate Create(IMeasure numerator, IMeasurement denominatorMeasurement, ILimit limit)
    {
        return new LimitedRate(this, numerator, denominatorMeasurement, limit);
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

    #region Override methods
    public override ILimitedRate Create(params IBaseMeasure[] baseMeasures)
    {
        string paramName = nameof(baseMeasures);
        int count = baseMeasures?.Length ?? 0;

        if (count < 2 || count > 3) throw QuantityArgumentOutOfRangeException(paramName, count);

        IMeasure numerator = GetValidRateParam<IMeasure>(baseMeasures![0], paramName);
        IDenominator denominator = GetValidRateParam<IDenominator>(baseMeasures[1], paramName);
        ILimit limit = count == 3 && baseMeasures[2] is IBaseMeasure baseMeasure ?
            GetValidRateParam<ILimit>(baseMeasure, paramName)
            : LimitFactory.Create(denominator, default);

        return Create(numerator, denominator, limit);
    }

    public override ILimitedRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));
        IMeasurement measurement = GetValidRateParam<IMeasurement>(denominatorMeasurement, nameof(denominatorMeasurement));
        ILimit limit = LimitFactory.Create(measurement, default(ulong), default);

        return Create(measure, measurement, limit);
    }

    public override ILimitedRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));
        IDenominator denominator = DenominatorFactory.Create(denominatorMeasureUnit);
        ILimit limit = LimitFactory.Create(denominator, default);

        return Create(measure, denominator, limit);
    }

    public override ILimitedRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));
        ILimit limit = (ILimit)(LimitFactory.CreateDefault(denominatorMeasureUnitCode)
            ?? throw InvalidMeasureUnitCodeEnumArgumentException(denominatorMeasureUnitCode, nameof(denominatorMeasureUnitCode)));

        return Create(measure, denominatorMeasureUnitCode, limit);
    }

    public override ILimitedRate CreateNew(IRate other)
    {
        if (other is ILimitedRate limitedRate) return CreateNew(limitedRate);

        IDenominator denominator = NullChecked(other, nameof(other)).Denominator;
        ILimit limit = LimitFactory.Create(denominator, default);

        return Create(other, limit);
    }
    #endregion
    #endregion
}
