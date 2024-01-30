namespace CsabaDu.FooVaria.Rates.Factories.Implementations;

public sealed class FlatRateFactory : RateFactory, IFlatRateFactory
{
    #region Constructors
    public FlatRateFactory(IDenominatorFactory denominatorFactory) : base(denominatorFactory)
    {
    }
    #endregion

    #region Public methods
    public IFlatRate Create(IMeasure numerator, string name, ValueType denominatorQuantity)
    {
        IDenominator denominator = DenominatorFactory.Create(name, denominatorQuantity);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, string name)
    {
        IDenominator denominator = DenominatorFactory.Create(name);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity)
    {
        return new FlatRate(this, numerator, denominatorMeasureUnit, denominatorQuantity);
    }

    public IFlatRate Create(IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        return new FlatRate(this, numerator, denominatorMeasureUnitCode);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement denominatorMeasurement)
    {
        return new FlatRate(this, numerator, denominatorMeasurement);
    }

    public IFlatRate Create(IMeasure numerator, IDenominator denominator)
    {
        return new FlatRate(this, numerator, denominator);
    }

    public IFlatRate Create(IRate rate)
    {
        return new FlatRate(this, rate);
    }

    public IFlatRate CreateNew(IFlatRate other)
    {
        return new FlatRate(other);
    }

    #region Override methods
    public override IFlatRate Create(params IBaseMeasure[] rateComponents)
    {
        string paramName = nameof(rateComponents);
        int count = rateComponents?.Length ?? 0;

        if (count != 2) throw QuantityArgumentOutOfRangeException(paramName, count);

        IMeasure numerator = GetValidRateParam<IMeasure>(rateComponents![0], paramName);
        IDenominator denominator = GetValidRateParam<IDenominator>(rateComponents[1], paramName);

        return Create(numerator, denominator);
    }

    public override IFlatRate CreateBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));
        IMeasurement measurement = GetValidRateParam<IMeasurement>(denominatorMeasurement, nameof(denominatorMeasurement));

        return Create(measure, measurement);
    }

    public override IFlatRate CreateBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));
        IDenominator denominator = DenominatorFactory.Create(denominatorMeasureUnit);

        return Create(measure, denominator);
    }

    public override IFlatRate CreateBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));

        return Create(measure, denominatorMeasureUnitCode);
    }

    public override IFlatRate CreateNew(IRate other)
    {
        return Create(other);
    }
    #endregion
    #endregion
}
