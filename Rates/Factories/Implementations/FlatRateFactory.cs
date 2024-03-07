namespace CsabaDu.FooVaria.Rates.Factories.Implementations;

public sealed class FlatRateFactory(IDenominatorFactory denominatorFactory) : RateFactory(denominatorFactory), IFlatRateFactory
{
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

    public IFlatRate Create(IMeasure numerator, Enum denominatorContext, ValueType denominatorQuantity)
    {
        MeasureUnitElements denominatorElements = new(denominatorContext, nameof(denominatorContext));
        Enum measureUnit = denominatorElements.MeasureUnit;
        IDenominator denominator = DenominatorFactory.Create(measureUnit);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, MeasureUnitCode denominatorCode)
    {
        IDenominator denominator = DenominatorFactory.Create(denominatorCode);

        return Create(numerator, denominator);
    }

    public IFlatRate Create(IMeasure numerator, IMeasurement denominator)
    {
        IDenominator baseMeasure = DenominatorFactory.Create(denominator);

        return Create(numerator, baseMeasure);
    }

    public IFlatRate Create(IMeasure numerator, IDenominator denominator)
    {
        return new FlatRate(this, numerator, denominator);
    }

    public IFlatRate CreateNew(IFlatRate other)
    {
        return new FlatRate(other);
    }

    #region Override methods
    public override IFlatRate Create(params IBaseMeasure[] rateComponents)
    {
        const string paramName = nameof(rateComponents);
        int count = rateComponents?.Length ?? 0;

        if (count != 2) throw QuantityArgumentOutOfRangeException(paramName, count);

        IMeasure numerator = GetValidNumerator(rateComponents![0], paramName);
        IBaseMeasure baseMeasure = rateComponents[1];

        if (baseMeasure is not IDenominator denominator)
        {
            denominator = CreateDenominator(baseMeasure);
        }

        return Create(numerator, denominator);
    }

    public override IFlatRate CreateBaseRate(IQuantifiable numerator, Enum denominator)
    {
        IMeasure measure = GetValidRateParam<IMeasure>(numerator, nameof(numerator));
        IDenominator baseMeasure = DenominatorFactory.Create(denominator);

        return Create(measure, baseMeasure);
    }

    public override IFlatRate CreateNew(IRate other)
    {
        if (other is IFlatRate flatRate) return CreateNew(flatRate);

        return new FlatRate(this, other);
    }
    #endregion
    #endregion
}
