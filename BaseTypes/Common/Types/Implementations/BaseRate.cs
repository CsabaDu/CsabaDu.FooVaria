//namespace CsabaDu.FooVaria.Common.Types.Implementations;

//public abstract class BaseRate : Quantifiable, IBaseRate
//{
//    #region Constructors
//    protected BaseRate(IBaseRate other) : base(other)
//    {
//    }

//    protected BaseRate(IBaseRateFactory factory, MeasureUnitCode denominatorMeasureUnitCode) : base(factory, denominatorMeasureUnitCode)
//    {
//    }

//    protected BaseRate(IBaseRateFactory factory, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
//    {
//    }

//    protected BaseRate(IBaseRateFactory factory, IBaseMeasurement denominatorMeasurement) : base(factory, denominatorMeasurement)
//    {
//    }

//    protected BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
//    {
//    }

//    protected BaseRate(IBaseRateFactory factory, IBaseMeasure denominator) : base(factory, denominator)
//    {
//    }
//    #endregion

//    #region Properties
//    public MeasureUnitCode? this[RateComponentCode rateComponentCode] => rateComponentCode switch
//    {
//        RateComponentCode.Numerator => GetNumeratorMeasureUnitCode(),
//        RateComponentCode.Denominator => GetDenominatorMeasureUnitCode(),

//        _ => null,
//    };
//    #endregion

//    #region Public methods
//    public int CompareTo(IBaseRate? other)
//    {
//        if (other == null) return 1;

//        ValidateMeasureUnitCodes(other!);

//        return DefaultQuantity.CompareTo(other.GetDefaultQuantity());
//    }

//    public bool Equals(IBaseRate? other)
//    {
//        return IsExchangeableTo(other)
//            && other!.DefaultQuantity == DefaultQuantity;
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasure denominator)
//    {
//        return GetFactory().CreateBaseRate(numerator, denominator);
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, IBaseMeasurement denominatorMeasurement)
//    {
//        return GetFactory().CreateBaseRate(numerator, denominatorMeasurement);
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
//    {
//        return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnit);
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
//    {
//        return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnitCode);
//    }

//    public IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures)
//    {
//        return GetFactory().CreateBaseRate(baseMeasures);
//    }

//    public MeasureUnitCode GetDenominatorMeasureUnitCode()
//    {
//        return MeasureUnitCode;
//    }

//    public decimal GetQuantity()
//    {
//        return DefaultQuantity;
//    }

//    public decimal ProportionalTo(IBaseRate other)
//    {
//        decimal defaultQuantity = NullChecked(other, nameof(other)).GetDefaultQuantity();

//        ValidateMeasureUnitCodes(other!);

//        return Math.Abs(DefaultQuantity / defaultQuantity);
//    }

//    #region Override methods
//    public override bool Equals(object? obj)
//    {
//        return obj is IBaseRate baseRate && Equals(baseRate);
//    }

//    public override IBaseRateFactory GetFactory()
//    {
//        return (IBaseRateFactory)Factory;
//    }

//    public override int GetHashCode()
//    {
//        return HashCode.Combine(DefaultQuantity, MeasureUnitCode, GetNumeratorMeasureUnitCode());
//    }

//    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
//    {
//        yield return GetNumeratorMeasureUnitCode();
//        yield return MeasureUnitCode;
//    }

//    #region Sealed methods
//    public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
//    {
//        base.ValidateMeasureUnit(measureUnit, paramName);
//    }

//    public override sealed TypeCode GetQuantityTypeCode()
//    {
//        return GetQuantityTypeCode(this);
//    }

//    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
//    {
//        if (GetMeasureUnitCodes().Contains(measureUnitCode)) return;

//        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
//    }
//    #endregion
//    #endregion

//    #region Abstract methods
//    public abstract IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode);
//    public abstract MeasureUnitCode GetNumeratorMeasureUnitCode();
//    #endregion
//    #endregion

//    #region Protected methods
//    protected void ValidateMeasureUnitCodes([DisallowNull] IBaseRate other)
//    {
//        string paramName = nameof(other);

//        ValidateMeasureUnitCode(other.MeasureUnitCode, paramName);

//        MeasureUnitCode numeratorMeasureUnitCode = other.GetNumeratorMeasureUnitCode();

//        if (numeratorMeasureUnitCode == GetNumeratorMeasureUnitCode()) return;

//        throw InvalidMeasureUnitCodeEnumArgumentException(numeratorMeasureUnitCode, paramName);
//    }

//    protected bool IsExchangeableTo(IBaseRate? baseRate)
//    {
//        return baseRate?.HasMeasureUnitCode(MeasureUnitCode) == true
//            && baseRate.GetNumeratorMeasureUnitCode() == (MeasureUnitCode);
//    }
//    #endregion
//}

