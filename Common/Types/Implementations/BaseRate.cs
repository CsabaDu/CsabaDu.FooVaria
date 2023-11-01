namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseRate : BaseMeasurable, IBaseRate
{
    #region Constructors
    public decimal DefaultQuantity { get; }

    public BaseRate(IBaseRate other) : base(other)
    {
        DefaultQuantity = other.DefaultQuantity;
    }

    public BaseRate(IBaseRateFactory factory, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, denominatorMeasureUnitTypeCode)
    {
        DefaultQuantity = defaultQuantity;
    }

    public BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
        DefaultQuantity = baseRate.DefaultQuantity;
    }

    public BaseRate(IBaseRateFactory factory, IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, denominatorMeasureUnitTypeCode)
    {
        DefaultQuantity = NullChecked(numerator, nameof(numerator)).DefaultQuantity;
    }

    public BaseRate(IBaseRateFactory factory, IQuantifiable numerator, IBaseMeasurable denominator) : base(factory, denominator)
    {
        DefaultQuantity = NullChecked(numerator, nameof(numerator)).DefaultQuantity;
    }

    public BaseRate(IBaseRateFactory factory, IQuantifiable numerator, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
    {
        DefaultQuantity = NullChecked(numerator, nameof(numerator)).DefaultQuantity;
    }


    #endregion

    #region Public methods
    public int CompareTo(IBaseRate? other)
    {
        if (other == null) return 1;

        if (AreExchangeables(this, other)) return DefaultQuantity.CompareTo(other.DefaultQuantity);

        throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, nameof(other));
    }

    public bool Equals(IBaseRate? other)
    {
        return AreExchangeables(this, other)
            && other!.DefaultQuantity == DefaultQuantity;
    }
    
    public decimal GetQuantity()
    {
        return DefaultQuantity;
    }

    public decimal ProportionalTo(IBaseRate other)
    {
        string name = nameof(other);
        decimal quantity = NullChecked(other, name).DefaultQuantity;

        if (quantity == 0) throw QuantityArgumentOutOfRangeException(name, quantity);

        if (AreExchangeables(this, other)) return Math.Abs(DefaultQuantity / quantity);

        throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, name);
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
        return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode/*, NumeratorMeasureUnitTypeCode*/);
    }


    public override void Validate(IFooVariaObject? fooVariaObject)
    {
        ValidateCommonBaseAction = () => _ = GetValidBaseRate(this, fooVariaObject!);

        Validate(this, fooVariaObject);
    }

    public override void ValidateMeasureUnit(Enum measureUnit, string paramName)
    {
        base.ValidateMeasureUnit(measureUnit, paramName);
    }

    public override void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode, string paramName)
    {
        base.ValidateMeasureUnitTypeCode(measureUnitTypeCode, paramName);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    public abstract IBaseRate GetBaseRate(IQuantifiable numerator, IBaseMeasurable denominator);
    public abstract IBaseRate GetBaseRate(IQuantifiable numerator, Enum denominatorMeasureUnit);
    public abstract MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();
    public abstract void ValidateQuantity(ValueType? quantity, string paramName);
    #endregion

    #region Static methods
    public static bool AreExchangeables(IBaseRate baseRate, IBaseMeasurable? baseMeasurable)
    {
        if (baseMeasurable is not IBaseRate other) return baseMeasurable?.IsExchangeableTo(baseRate.MeasureUnitTypeCode) == true;

        return AreExchangeables(baseRate, other);
    }

    public static bool AreExchangeables(IBaseRate? baseRate, IBaseRate? other)
    {
        if (baseRate == null || other == null) return false;

        return baseRate.MeasureUnitTypeCode == other.MeasureUnitTypeCode
            && baseRate.GetNumeratorMeasureUnitTypeCode() == other.GetNumeratorMeasureUnitTypeCode();
    }

    public static int Compare(IBaseRate? baseRate, IBaseRate? other)
    {
        if (baseRate == null && other == null) return 0;

        if (baseRate == null) return -1;

        return baseRate.CompareTo(other);
    }

    public static bool Equals(IBaseRate baseRate, IBaseRate? other)
    {
        return baseRate?.Equals(other) == true;
    }

    public static decimal Proportionals(IBaseRate baseRate, IBaseRate other)
    {
        return NullChecked(baseRate, nameof(baseRate)).ProportionalTo(other);
    }
    #endregion
    #endregion

    #region Private methods
    private ArgumentOutOfRangeException BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(IBaseRate baseRate, string name)
    {
        MeasureUnitTypeCode measureUnitTypeCode = baseRate.MeasureUnitTypeCode;

        if (!IsExchangeableTo(measureUnitTypeCode))
        {
            throw exception();
        }
        else
        {
            measureUnitTypeCode = baseRate.GetNumeratorMeasureUnitTypeCode();

            throw exception();
        }

        #region Local methods
        ArgumentOutOfRangeException exception()
        {
            return new ArgumentOutOfRangeException(name, measureUnitTypeCode, null);
        }
        #endregion
    }

    public IBaseRate GetBaseRate(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        return GetFactory().Create(numerator, denominatorMeasureUnitTypeCode);
    }
    #endregion

    protected static T GetValidBaseRate<T>(T commonBase, IFooVariaObject other) where T : class, IBaseRate // TODO 
    {
        T baseMeasurable = GetValidCommonBase(commonBase, other);

        commonBase.ValidateQuantity(baseMeasurable.DefaultQuantity, nameof(commonBase));

        MeasureUnitTypeCode measureUnitTypeCode = commonBase.MeasureUnitTypeCode;
        MeasureUnitTypeCode otherMeasureUnitTypeCode = baseMeasurable.MeasureUnitTypeCode;

        _ = GetValidBaseMeasurable(baseMeasurable, measureUnitTypeCode, otherMeasureUnitTypeCode);

        measureUnitTypeCode = commonBase.GetNumeratorMeasureUnitTypeCode();
        otherMeasureUnitTypeCode = baseMeasurable.GetNumeratorMeasureUnitTypeCode();

        return GetValidBaseMeasurable(baseMeasurable, measureUnitTypeCode, otherMeasureUnitTypeCode);
    }
}
