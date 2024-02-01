using CsabaDu.FooVaria.BaseTypes.Common.Statics;
using CsabaDu.FooVaria.BaseTypes.Measurables.Statics;
using CsabaDu.FooVaria.BaseTypes.Quantifiables.Statics;

namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Types.Implementations;

public abstract class BaseRate : Quantifiable, IBaseRate
{
    //public abstract decimal DefaultQuantity { get; init; }
    #region Constructors
    protected BaseRate(IBaseRate other) : base(other)
    {
    }

    protected BaseRate(IBaseRateFactory factory, MeasureUnitCode denominatorMeasureUnitCode) : base(factory, denominatorMeasureUnitCode)
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

    protected BaseRate(IBaseRateFactory factory, IQuantifiable denominator) : base(factory, denominator)
    {
    }
    #endregion

    #region Properties
    public MeasureUnitCode? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => GetNumeratorMeasureUnitCode(),
        RateComponentCode.Denominator => GetDenominatorMeasureUnitCode(),

        _ => null,
    };
    #endregion

    #region Public methods
    public int CompareTo(IBaseRate? other)
    {
        if (other == null) return 1;

        ValidateMeasureUnitCodes(other!);

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    public bool Equals(IBaseRate? other)
    {
        return IsExchangeableTo(other)
            && other!.GetDefaultQuantity() == GetDefaultQuantity();
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

    public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
    {
        return GetFactory().CreateBaseRate(numerator, denominatorMeasureUnitCode);
    }

    public IBaseRate GetBaseRate(params IBaseMeasure[] baseMeasures)
    {
        return GetFactory().CreateBaseRate(baseMeasures);
    }

    public MeasureUnitCode GetDenominatorMeasureUnitCode()
    {
        return MeasureUnitCode;
    }

    public decimal GetQuantity()
    {
        return GetDefaultQuantity();
    }

    public decimal ProportionalTo(IBaseRate other)
    {
        decimal defaultQuantity = NullChecked(other, nameof(other)).GetDefaultQuantity();

        ValidateMeasureUnitCodes(other!);

        return Math.Abs(GetDefaultQuantity() / defaultQuantity);
    }

    #region Override methods
    public override sealed bool Equals(object? obj)
    {
        return obj is IBaseRate baseRate && Equals(baseRate);
    }

    public override IBaseRateFactory GetFactory()
    {
        return (IBaseRateFactory)Factory;
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(MeasureUnitCode, GetNumeratorMeasureUnitCode(), GetDefaultQuantity());
    }

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        yield return GetNumeratorMeasureUnitCode();
        yield return MeasureUnitCode;
    }

    #region Sealed methods
    public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        base.ValidateMeasureUnit(measureUnit, paramName);
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return GetQuantityTypeCode(this);
    }

    public override sealed void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    {
        if (GetMeasureUnitCodes().Contains(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode);
    public abstract MeasureUnitCode GetNumeratorMeasureUnitCode();
    #endregion
    #endregion

    #region Protected methods
    protected void ValidateMeasureUnitCodes(IBaseRate other)
    {
        foreach (RateComponentCode item in Enum.GetValues<RateComponentCode>())
        {
            MeasureUnitCode? thisItem = this[item];
            MeasureUnitCode? otherItem = other[item];

            if (thisItem.HasValue
                && otherItem.HasValue
                && thisItem != otherItem)
            {
                throw InvalidMeasureUnitCodeEnumArgumentException(otherItem.Value, nameof(otherItem));
            }
        }

        //string paramName = nameof(baseRate);

        //ValidateMeasureUnitCode(baseRate.MeasureUnitCode, paramName);

        //MeasureUnitCode numeratorMeasureUnitCode = baseRate.GetNumeratorMeasureUnitCode();

        //if (numeratorMeasureUnitCode == GetNumeratorMeasureUnitCode()) return;

        //throw InvalidMeasureUnitCodeEnumArgumentException(numeratorMeasureUnitCode, paramName);
    }

    public bool IsExchangeableTo(IBaseRate? baseRate)
    {
        return baseRate?.HasMeasureUnitCode(MeasureUnitCode) == true
            && baseRate.GetNumeratorMeasureUnitCode() == GetNumeratorMeasureUnitCode();
    }

    public object GetQuantity(TypeCode quantityTypeCode)
    {
        object? quantity = GetQuantity().ToQuantity(Defined(quantityTypeCode, nameof(quantityTypeCode)));

        if (quantity != null) return quantity;

        throw new InvalidOperationException(null);
    }

    //public void ValidateQuantity(ValueType? quantity, TypeCode quantityTypeCode, string paramName)
    //{
    //    Type quantityType = NullChecked(quantity, paramName).GetType();

    //    if (Type.GetTypeCode(quantityType) == quantityTypeCode) return;

    //    throw QuantityArgumentOutOfRangeException(paramName, quantity!);
    //}

    public bool? FitsIn(ILimiter? limiter)
    {
        throw new NotImplementedException();
    }

    public bool? FitsIn(IBaseRate? baseRate, LimitMode? limitMode)
    {
        bool limitModeHasValue = limitMode.HasValue;

        if (baseRate == null && !limitModeHasValue) return true;

        if (!IsExchangeableTo(baseRate)) return null;

        int comparison = CompareTo(baseRate);

        if (!limitModeHasValue) return comparison <= 0;

        LimitMode limitModeValue = limitMode!.Value;

        if (!limitModeValue.IsDefined()) return null;

        return comparison.FitsIn(limitModeValue);
    }
    #endregion

    protected static bool? FitsIn(IBaseRate s, IBaseRate? baseRate, LimitMode? limitMode, Func<bool?> fitsIn)
    {
        bool limitModeHasValue = limitMode.HasValue;

        if (baseRate == null && !limitModeHasValue) return true;

        if (!s.IsExchangeableTo(baseRate)) return null;

        if (!limitModeHasValue) return s.CompareTo(baseRate) <= 0;

        LimitMode limitModeValue = limitMode!.Value;

        if (!limitModeValue.IsDefined()) return null;

        int comparison = s.CompareTo(baseRate);

        return comparison.FitsIn(limitMode!.Value);
    }
}
