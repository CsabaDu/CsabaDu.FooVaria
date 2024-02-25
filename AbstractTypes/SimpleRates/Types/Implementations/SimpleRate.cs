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

    protected SimpleRate(ISimpleRateFactory factory, IBaseRate other) : base(factory)
    {
        NumeratorCode = NullChecked(other, nameof(other)).GetNumeratorCode();
        DenominatorCode = other.GetDenominatorCode();
        DefaultQuantity = other.GetDefaultQuantity();
    }

    protected SimpleRate(ISimpleRateFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode) : base(factory)
    {
        NumeratorCode = Defined(numeratorCode, nameof(numeratorCode));
        DenominatorCode = Defined(denominatorCode, nameof(denominatorCode));
        DefaultQuantity = GetValidPositiveQuantity(defaultQuantity, nameof(defaultQuantity));
    }

    protected SimpleRate(ISimpleRateFactory factory, Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit) : base(factory)
    {
        NumeratorCode = GetMeasureUnitCode(numeratorMeasureUnit);
        DenominatorCode = getDenominatorCode(out decimal exchangeRate);
        DefaultQuantity = GetDefaultQuantity(quantity, exchangeRate);

        #region Local methods
        MeasureUnitCode getDenominatorCode(out decimal exchangeRate)
        {
            MeasureUnitCode denominatorCode = GetMeasureUnitCode(denominatorMeasureUnit);

            exchangeRate = GetExchangeRate(numeratorMeasureUnit, nameof(numeratorMeasureUnit));
            exchangeRate /= GetExchangeRate(denominatorMeasureUnit, nameof(denominatorMeasureUnit));

            return denominatorCode;
        }
        #endregion
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
    #region Override methods
    #region Sealed methods
    public override sealed IBaseRate GetBaseRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        return GetFactory().CreateSimpleRate(numeratorCode, defaultQuantity, denominatorCode)!;
    }

    public override sealed decimal GetDefaultQuantity()
    {
        return DefaultQuantity;
    }

    public override ISimpleRateFactory GetFactory()
    {
        return (ISimpleRateFactory)Factory;
    }

    public override sealed Enum GetMeasureUnit()
    {
        return NumeratorCode.GetDefaultMeasureUnit();
    }

    public override sealed MeasureUnitCode GetDenominatorCode()
    {
        return DenominatorCode;
    }

    public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes();
    }

    public override sealed MeasureUnitCode GetNumeratorCode()
    {
        return NumeratorCode;
    }

    public override sealed Enum GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode] ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }
    #endregion
    #endregion
    #endregion
}
