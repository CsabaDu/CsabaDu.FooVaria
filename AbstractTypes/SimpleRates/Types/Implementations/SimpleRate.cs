namespace CsabaDu.FooVaria.AbstractTypes.SimpleRates.Types.Implementations;

public abstract class SimpleRate : BaseRate, ISimpleRate
{
    #region Constructors
    protected SimpleRate(ISimpleRate other) : base(other)
    {
        NumeratorCode = other.NumeratorCode;
        DenominatorCode = other.DenominatorCode;
        DefaultQuantity = other.DefaultQuantity;
    }

    protected SimpleRate(ISimpleRateFactory factory, IBaseRate baseRate) : base(factory)
    {
        NumeratorCode = NullChecked(baseRate, nameof(baseRate)).GetNumeratorCode();
        DenominatorCode = baseRate.GetDenominatorCode();
        DefaultQuantity = baseRate.GetDefaultQuantity();
    }

    protected SimpleRate(ISimpleRateFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode) : base(factory)
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
        return GetFactory().MeasureFactory;
    }

    public ISimpleRate GetSimpleRate(MeasureUnitCode numeratorCode, decimal quantity, MeasureUnitCode denominatorCode)
    {
        return GetFactory().CreateSimpleRate(numeratorCode, quantity, denominatorCode);
    }

    public ISimpleRate GetSimpleRate(IQuantifiable numerator, IQuantifiable denominator)
    {
        decimal defaultQuantity = NullChecked(numerator, nameof(numerator)).GetDefaultQuantity();
        defaultQuantity /= NullChecked(denominator, nameof(denominator)).GetDefaultQuantity();

        return GetSimpleRate(numerator.GetMeasureUnitCode(), defaultQuantity, denominator.GetMeasureUnitCode());
    }

    public decimal GetQuantity(Enum context, string paramName)
    {
        MeasurementElements measurementElements = GetMeasurementElements(context);
        MeasureUnitCode measureUnitCode = measurementElements.GetMeasureUnitCode();

        if (!GetMeasureUnitCodes().Contains(measureUnitCode))
        {
            throw new InvalidEnumArgumentException(paramName, (int)(object)context, context.GetType());
        }

        decimal exchangeRate = measurementElements.ExchangeRate;

        if (exchangeRate == decimal.One) return DefaultQuantity;

        return measureUnitCode == NumeratorCode ?
            DefaultQuantity / exchangeRate
            : DefaultQuantity * exchangeRate;
    }

    public decimal GetQuantity(Enum numerator, string numeratorName, Enum denominator, string denominatorName)
    {
        MeasurementElements numeratorElements = GetValidMeasurementElements(numerator, RateComponentCode.Numerator, numeratorName);
        decimal numeratorExchangeRate = numeratorElements.ExchangeRate;
        MeasurementElements denominatorElements = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, denominatorName);
        decimal denominatorExchangeRate = denominatorElements.ExchangeRate;

        if (numeratorExchangeRate == denominatorExchangeRate) return DefaultQuantity;

        return DefaultQuantity / numeratorExchangeRate * denominatorExchangeRate;
    }

    public void ValidateDenominator(Enum denominator)
    {
        _ = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, nameof(denominator));
    }

    private MeasurementElements GetValidMeasurementElements(Enum context, RateComponentCode rateComponentCode, string paramName)
    {
        MeasurementElements measurementElements = GetMeasurementElements(context);
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(rateComponentCode);

        if (measureUnitCode == measurementElements.GetMeasureUnitCode()) return measurementElements;

        throw new InvalidEnumArgumentException(paramName, (int)(object)context, context.GetType());
    }

    #region Override methods
    public override ISimpleRateFactory GetFactory()
    {
        return (ISimpleRateFactory)Factory;
    }

    #region Sealed methods
    public override sealed decimal GetDefaultQuantity()
    {
        return DefaultQuantity;
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        return NumeratorCode.GetDefaultMeasureUnit();
    }

    public override sealed MeasureUnitCode GetDenominatorCode()
    {
        return DenominatorCode;
    }

    public override sealed MeasureUnitCode GetMeasureUnitCode(RateComponentCode rateComponentCode)
    {
        return GetRateComponent(rateComponentCode) is MeasureUnitCode measureUnitCode ?
            measureUnitCode
            : throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return GetRateComponentCodes().Where(x => this[x] is MeasureUnitCode).Select(GetMeasureUnitCode);
    }

    public override sealed MeasureUnitCode GetNumeratorCode()
    {
        return NumeratorCode;
    }

    public override sealed Enum? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode];
    }
    #endregion
    #endregion
    #endregion
}
