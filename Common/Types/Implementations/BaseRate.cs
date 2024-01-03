namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseRate : BaseMeasure, IBaseRate
{
    #region Constructors
    protected BaseRate(IBaseRate other) : base(other)
    {
    }

    protected BaseRate(IBaseRateFactory factory, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, denominatorMeasureUnitTypeCode)
    {
    }

    protected BaseRate(IBaseRateFactory factory, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
    {
    }

    protected BaseRate(IBaseRateFactory factory, IBaseMeasurement denominatorMeasurement) : base(factory, denominatorMeasurement)
    {
    }

    protected BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
    }

    protected BaseRate(IBaseRateFactory factory, IBaseMeasure denominator) : base(factory, denominator)
    {
    }
    #endregion

    #region Properties
    public MeasureUnitTypeCode? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => GetNumeratorMeasureUnitTypeCode(),
        RateComponentCode.Denominator => GetDenominatorMeasureUnitTypeCode(),

        _ => null,
    };
    #endregion

    #region Public methods
    public int CompareTo(IBaseRate? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitTypeCodes(other!);

        return DefaultQuantity.CompareTo(other.GetDefaultQuantity());
    }

    public bool Equals(IBaseRate? other)
    {
        return IsExchangeableTo(other)
            && other!.DefaultQuantity == DefaultQuantity;
    }

    public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasure denominator)
    {
        return GetFactory().CreateBaseRate(numerator, denominator);
    }

    public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
    {
        return GetFactory().CreateBaseRate(numerator, denominatorMeasurement);
    }

    public IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
    {
        return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnit);
    }

    public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnitTypeCode);
    }

    public IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures)
    {
        return GetFactory().CreateBaseRate(baseMeasures);
    }

    public MeasureUnitTypeCode GetDenominatorMeasureUnitTypeCode()
    {
        return MeasureUnitTypeCode;
    }

    public decimal GetQuantity()
    {
        return DefaultQuantity;
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseRate baseRate && Equals(baseRate);
    }

    public override IBaseRateFactory GetFactory()
    {
        return (IBaseRateFactory)Factory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode, GetNumeratorMeasureUnitTypeCode());
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return GetQuantityTypeCode(this);
    }

    public decimal ProportionalTo(IBaseRate other)
    {
        decimal defaultQuantity = NullChecked(other, nameof(other)).GetDefaultQuantity();

        ValidateMeasureUnitTypeCodes(other!);

        return Math.Abs(DefaultQuantity / defaultQuantity);
    }

    public override sealed void Validate(IRootObject? rootObject, string paramName)
    {
        Validate(this, rootObject, validateBaseRate, paramName);

        #region Local methods
        void validateBaseRate()
        {
            _ = GetValidBaseRate(this, rootObject!, paramName);
        }
        #endregion
    }

    public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        base.ValidateMeasureUnit(measureUnit, paramName);
    }

    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        yield return GetNumeratorMeasureUnitTypeCode();
        yield return MeasureUnitTypeCode;
    }

    public override sealed void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
    {
        if (GetMeasureUnitTypeCodes().Contains(measureUnitTypeCode)) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode, paramName);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    public abstract MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();
    #endregion
    #endregion

    #region Protected methods
    protected void ValidateMeasureUnitTypeCodes([DisallowNull] IBaseRate other)
    {
        string paramName = nameof(other);

        ValidateMeasureUnitTypeCode(other.MeasureUnitTypeCode, paramName);

        MeasureUnitTypeCode numeratorMeasureUnitTypeCode = other.GetNumeratorMeasureUnitTypeCode();

        if (numeratorMeasureUnitTypeCode == GetNumeratorMeasureUnitTypeCode()) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(numeratorMeasureUnitTypeCode, paramName);
    }

    protected bool IsExchangeableTo(IBaseRate? baseRate)
    {
        return baseRate?.HasMeasureUnitTypeCode(MeasureUnitTypeCode) == true
            && baseRate.GetNumeratorMeasureUnitTypeCode() == (MeasureUnitTypeCode);
    }

    #region Static methods
    protected static T GetValidBaseRate<T>(T commonBase, IRootObject other, string paramName)
        where T : class, IBaseRate
    {
        T baseRate = GetValidMeasurable(commonBase, other, paramName);

        commonBase.ValidateQuantity(baseRate.GetDefaultQuantity(), paramName);

        MeasureUnitTypeCode measureUnitTypeCode = commonBase.GetNumeratorMeasureUnitTypeCode();
        MeasureUnitTypeCode otherMeasureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();

        _ = GetValidBaseMeasurable(baseRate, measureUnitTypeCode, otherMeasureUnitTypeCode, paramName);

        return baseRate;
    }
    #endregion
    #endregion
}

