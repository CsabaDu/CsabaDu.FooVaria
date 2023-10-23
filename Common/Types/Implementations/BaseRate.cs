namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseRate : BaseMeasurable, IBaseRate
{
    #region Constructors
    public virtual decimal DefaultQuantity { get; }

    internal BaseRate(IBaseRate other) : base(other)
    {
        //NumeratorMeasureUnitTypeCode  = other.NumeratorMeasureUnitTypeCode;
        DefaultQuantity = other.DefaultQuantity;
    }

    internal BaseRate(IBaseRateFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        //NumeratorMeasureUnitTypeCode = getValidNumeratorMeasureUnitTypeCode();
        DefaultQuantity = defaultQuantity;

        //#region Local methods
        //MeasureUnitTypeCode getValidNumeratorMeasureUnitTypeCode()
        //{
        //    try
        //    {
        //        ValidateMeasureUnitTypeCode(numeratorMeasureUnitTypeCode);
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(numeratorMeasureUnitTypeCode), numeratorMeasureUnitTypeCode, null);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new InvalidOperationException(ex.Message, ex.InnerException);
        //    }

        //    return numeratorMeasureUnitTypeCode;
        //}
        //#endregion
    }

    internal BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
        //NumeratorMeasureUnitTypeCode = baseRate.NumeratorMeasureUnitTypeCode;
        DefaultQuantity = baseRate.DefaultQuantity;
    }

    internal BaseRate(IBaseRateFactory factory, IQuantifiable numerator, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        //NumeratorMeasureUnitTypeCode = getValidNumeratorMeasureUnitTypeCode();
        DefaultQuantity = numerator.DefaultQuantity;

        #region Local methods
        MeasureUnitTypeCode getValidNumeratorMeasureUnitTypeCode()
        {
            string name = nameof(numerator);

            if (NullChecked(numerator, name) is not IBaseMeasurable baseMeasurable
                || numerator is IBaseRate)
            {
                throw ArgumentTypeOutOfRangeException(name, numerator);
            }

            return baseMeasurable.MeasureUnitTypeCode;
        }
        #endregion
    }
    #endregion

    #region Properties
    //public MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; }
    #endregion

    #region Public methods
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

    public override void ValidateMeasureUnit(Enum measureUnit)
    {
        base.ValidateMeasureUnit(measureUnit);
    }

    public override void ValidateMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        base.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
    }
    #endregion

    #region Virtual methods
    public virtual int CompareTo(IBaseRate? other)
    {
        if (other == null) return 1;

        if (AreExchangeables(this, other)) return DefaultQuantity.CompareTo(other.DefaultQuantity);

        throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, nameof(other));
    }

    public virtual bool Equals(IBaseRate? other)
    {
        return other?.DefaultQuantity == DefaultQuantity
            && AreExchangeables(this, other);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    //{
    //    return new BaseRate(GetFactory(), numeratorMeasureUnitTypeCode, defaultQuantity, denominatorMeasureUnitTypeCode);
    //}

    public abstract MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode();
    #endregion

    #region Static methods
    public static bool AreExchangeables(IBaseRate baseRate, IBaseMeasurable? baseMeasurable)
    {
        if (baseMeasurable is not IBaseRate other) return baseMeasurable?.IsExchangeableTo(baseRate.MeasureUnitTypeCode) == true;

        return AreExchangeables(baseRate, other);
    }

    public static bool AreExchangeables(IBaseRate baseRate, IBaseRate other)
    {
        return baseRate.MeasureUnitTypeCode == other.MeasureUnitTypeCode
            && baseRate.GetNumeratorMeasureUnitTypeCode() == other.GetNumeratorMeasureUnitTypeCode();
    }
    public static int Compare(IBaseRate baseRate, IBaseRate? other)
    {
        return NullChecked(baseRate, nameof(baseRate)).CompareTo(other);
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
            throw exception(name, measureUnitTypeCode);
        }

        throw exception(name, baseRate.GetNumeratorMeasureUnitTypeCode());

        #region Local methods
        static ArgumentOutOfRangeException exception(string name, MeasureUnitTypeCode measureUnitTypeCode)
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
        MeasureUnitTypeCode measureUnitTypeCode = commonBase.MeasureUnitTypeCode;
        MeasureUnitTypeCode otherMeasureUnitTypeCode = baseMeasurable.MeasureUnitTypeCode;

        _ = GetValidBaseMeasurable(baseMeasurable, measureUnitTypeCode, otherMeasureUnitTypeCode);

        measureUnitTypeCode = commonBase.GetNumeratorMeasureUnitTypeCode();
        otherMeasureUnitTypeCode = baseMeasurable.GetNumeratorMeasureUnitTypeCode();

        return GetValidBaseMeasurable(baseMeasurable, measureUnitTypeCode, otherMeasureUnitTypeCode);
    }
}
