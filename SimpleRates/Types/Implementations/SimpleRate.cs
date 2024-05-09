namespace CsabaDu.FooVaria.SimpleRates.Types.Implementations;

public abstract class SimpleRate : BaseRate, ISimpleRate
{
    #region Constructors
    protected SimpleRate(ISimpleRateFactory factory, IBaseRate baseRate) : base(factory, nameof(factory))
    {
        NumeratorCode = NullChecked(baseRate, nameof(baseRate)).GetNumeratorCode();
        DenominatorCode = baseRate.GetDenominatorCode();
        DefaultQuantity = baseRate.GetDefaultQuantity();
    }

    protected SimpleRate(ISimpleRateFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode) : base(factory, nameof(factory))
    {
        NumeratorCode = Defined(numeratorCode, nameof(numeratorCode));
        DenominatorCode = Defined(denominatorCode, nameof(denominatorCode));
        DefaultQuantity = GetValidPositiveQuantity(defaultQuantity, nameof(defaultQuantity));
    }
    #endregion

    #region Properties
    public MeasureUnitCode NumeratorCode { get; init; }
    public MeasureUnitCode DenominatorCode { get; init; }
    public decimal DefaultQuantity { get; init; }
    public Enum? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => NumeratorCode,
        RateComponentCode.Denominator => DenominatorCode,
        RateComponentCode.Limit => GetLimitMode(),

        _ => null,
    };
    #endregion

    #region Public methods
    public IMeasure Denominate(Enum denominator)
    {
        MeasurementElements measurementElements = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, nameof(denominator));

        decimal quantity = DefaultQuantity * measurementElements.ExchangeRate;

        return GetMeasureFactory().Create(denominator, quantity);
    }

    public IMeasureFactory GetMeasureFactory()
    {
        return GetSimpleRateFactory().MeasureFactory;
    }

    public ISimpleRate GetSimpleRate(MeasureUnitCode numeratorCode, decimal quantity, MeasureUnitCode denominatorCode)
    {
        return GetSimpleRateFactory().CreateSimpleRate(numeratorCode, quantity, denominatorCode);
    }

    public decimal GetQuantity(Enum context, string paramName)
    {
        MeasurementElements measurementElements = GetMeasurementElements(context, paramName);
        MeasureUnitCode measureUnitCode = measurementElements.MeasureUnitCode;
        decimal exchangeRate = measurementElements.ExchangeRate;

        if (measureUnitCode == DenominatorCode) return DefaultQuantity * exchangeRate;

        if (measureUnitCode == NumeratorCode) return DefaultQuantity / exchangeRate;

        throw new InvalidEnumArgumentException(paramName, (int)(object)context, context.GetType());
    }

    public decimal GetQuantity(Enum numerator, string numeratorName, Enum denominator, string denominatorName)
    {
        MeasurementElements measurementElements = GetValidMeasurementElements(numerator, RateComponentCode.Numerator, numeratorName);
        decimal numeratorExchangeRate = measurementElements.ExchangeRate;
        measurementElements = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, denominatorName);
        decimal denominatorExchangeRate = measurementElements.ExchangeRate;

        if (numeratorExchangeRate == denominatorExchangeRate) return DefaultQuantity;

        return DefaultQuantity / numeratorExchangeRate * denominatorExchangeRate;
    }

    public void ValidateDenominator(Enum denominator, string paramName)
    {
        _ = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, paramName);
    }

    #region Override methods
    #region Sealed methods
    public override sealed decimal GetDefaultQuantity()
    {
        return DefaultQuantity;
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        return NumeratorCode.GetDefaultMeasureUnit()!;
    }

    //public override sealed MeasureUnitCode GetDenominatorCode()
    //{
    //    return DenominatorCode;
    //}

    //public override sealed MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    //{
    //    if (GetRateComponent(rateComponentCode) is MeasureUnitCode measureUnitCode) return measureUnitCode;

    //    throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    //}

    //public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    //{
    //    return GetRateComponentCodes().Where(x => this[x] is MeasureUnitCode).Select(GetMeasureUnitCode);
    //}

    //public override sealed MeasureUnitCode GetNumeratorCode()
    //{
    //    return NumeratorCode;
    //}

    public override sealed Enum? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode];
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    private ISimpleRateFactory GetSimpleRateFactory()
    {
        return (ISimpleRateFactory)GetFactory();
    }

    private MeasurementElements GetValidMeasurementElements(Enum context, RateComponentCode rateComponentCode, string paramName)
    {
        MeasurementElements measurementElements = GetMeasurementElements(context, paramName);
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(rateComponentCode);

        if (measureUnitCode == measurementElements.MeasureUnitCode) return measurementElements;

        throw new InvalidEnumArgumentException(paramName, (int)(object)context, context.GetType());
    }
    #endregion
}
