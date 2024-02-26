namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Types.Implementations;

public abstract class Quantifiable : BaseQuantifiable, IQuantifiable
{
    #region Constructors
    protected Quantifiable(IQuantifiable other) : base(other)
    {
    }

    protected Quantifiable(IQuantifiableFactory factory) : base(factory)
    {
    }
    #endregion

    #region Properties
    //public MeasureUnitCode MeasureUnitCode { get; init; }
    #endregion

    #region Public methods
    public bool Equals(IQuantifiable? other)
    {
        return base.Equals(other);
    }

    public virtual bool? FitsIn(ILimiter? limiter)
    {
        if (limiter is not IQuantifiable quantifiable) return null;

        LimitMode? limitMode = limiter.GetLimitMode();

        return FitsIn(quantifiable, limitMode);
    }

    public bool IsExchangeableTo(Enum? context)
    {
        if (context == null) return false;

        if (context is not MeasureUnitCode measureUnitCode)
        {
            if (!IsDefinedMeasureUnit(context)) return false;

            measureUnitCode = GetDefinedMeasureUnitCode(context);
        }

        return HasMeasureUnitCode(measureUnitCode);
    }

    public decimal ProportionalTo(IQuantifiable? other)
    {
        string paramName = nameof(other);

        ValidateMeasureUnitCode(other, paramName);

        decimal defaultQuantity = other!.GetDefaultQuantity();

        if (defaultQuantity != 0) return Math.Abs(GetDefaultQuantity() / defaultQuantity);

        throw QuantityArgumentOutOfRangeException(paramName, defaultQuantity);
    }

    public void ValidateQuantifiable(IBaseQuantifiable? baseQuantifiable, string paramName)
    {
        ValidateMeasureUnitCode(baseQuantifiable, paramName);
        ValidateQuantity(baseQuantifiable, paramName);
    }

    #region Override methods
    public override IQuantifiableFactory GetFactory()
    {
        return (IQuantifiableFactory)Factory;
    }

    #region Sealed methods
    public override sealed MeasureUnitCode GetMeasureUnitCode()
    {
        return base.GetMeasureUnitCode();
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return base.GetQuantityTypeCode();
    }

    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        base.ValidateMeasureUnitCode(measureUnitCode, paramName);
    }
    #endregion
    #endregion

    public virtual int CompareTo(IQuantifiable? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitCode(other.GetMeasureUnitCode(), nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    #region Abstract methods
    public abstract IQuantifiable? ExchangeTo(Enum? context);
    public abstract bool? FitsIn(IQuantifiable? other, LimitMode? limitMode);
    public abstract IQuantifiable GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
    public abstract object GetQuantity(RoundingMode roundingMode);
    public abstract IQuantifiable Round(RoundingMode roundingMode);
    #endregion
    #endregion
}
