namespace CsabaDu.FooVaria.Rates.Factories.Implementations;

public sealed class LimitedRateFactory : RateFactory, ILimitedRateFactory
{
    public LimitedRateFactory(IDenominatorFactory denominatorFactory, ILimitFactory limitFactory) : base(denominatorFactory)
    {
        LimitFactory = NullChecked(limitFactory, nameof(limitFactory));
    }

    public ILimitFactory LimitFactory { get; init; }

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

    public ILimitedRate Create(IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode, ILimit limit)
    {
        return new LimitedRate(this, numerator, denominatorMeasureUnitTypeCode, limit);
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

    public override ILimitedRate Create(params IRateComponent[] rateComponents)
    {
        string paramName = nameof(rateComponents);
        int count = rateComponents?.Length ?? 0;

        if (count < 2 || count > 3) throw QuantityArgumentOutOfRangeException(paramName, count);

        IMeasure numerator = GetValidRateParam<IMeasure>(rateComponents![0], paramName);
        IDenominator denominator = GetValidRateParam<IDenominator>(rateComponents[1], paramName);
        ILimit limit = count == 3 && rateComponents[2] is IRateComponent rateComponent ?
            GetValidRateParam<ILimit>(rateComponent, paramName)
            : (ILimit)LimitFactory.CreateNew(denominator);

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
        ILimit limit = (ILimit)LimitFactory.CreateNew(denominator);

        return Create(measure, denominator, limit);
    }

    public override ILimitedRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));
        ILimit limit = LimitFactory.CreateDefault(denominatorMeasureUnitTypeCode)
            ?? throw InvalidMeasureUnitTypeCodeEnumArgumentException(denominatorMeasureUnitTypeCode, nameof(denominatorMeasureUnitTypeCode));

        return Create(measure, denominatorMeasureUnitTypeCode, limit);
    }

    public ILimitedRate CreateNew(ILimitedRate other)
    {
        return new LimitedRate(other);
    }

    public override ILimitedRate CreateNew(IRate other)
    {
        if (other is ILimitedRate limitedRate) return CreateNew(limitedRate);

        IDenominator denominator = NullChecked(other, nameof(other)).Denominator;
        ILimit limit = (ILimit)LimitFactory.CreateNew(denominator);

        return Create(other, limit);
    }
}
