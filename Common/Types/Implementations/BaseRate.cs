using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Common.Types.Implementations;

public abstract class BaseRate : BaseMeasurable, IBaseRate
{
    #region Constructors
    public virtual decimal DefaultQuantity { get; }

    protected BaseRate(IBaseRate other) : base(other)
    {
        NumeratorMeasureUnitTypeCode  = other.NumeratorMeasureUnitTypeCode;
        DefaultQuantity = other.DefaultQuantity;
    }

    protected BaseRate(IBaseRateFactory factory, MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        NumeratorMeasureUnitTypeCode = getValidNumeratorMeasureUnitTypeCode();
        DefaultQuantity = defaultQuantity;

        #region Local methods
        MeasureUnitTypeCode getValidNumeratorMeasureUnitTypeCode()
        {
            try
            {
                ValidateMeasureUnitTypeCode(numeratorMeasureUnitTypeCode);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException(nameof(numeratorMeasureUnitTypeCode), numeratorMeasureUnitTypeCode, null);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message, ex.InnerException);
            }

            return numeratorMeasureUnitTypeCode;
        }
        #endregion
    }

    protected BaseRate(IBaseRateFactory factory, IBaseRate baseRate) : base(factory, baseRate)
    {
        NumeratorMeasureUnitTypeCode = baseRate.NumeratorMeasureUnitTypeCode;
        DefaultQuantity = baseRate.DefaultQuantity;
    }
    #endregion

    #region Properties
    public MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; }
    #endregion

    #region Public methods
    public decimal ProportionalTo(IBaseRate other)
    {
        string name = nameof(other);
        decimal quantity = NullChecked(other, name).DefaultQuantity;

        if (quantity == 0) throw QuantityArgumentOutOfRangeException(name, quantity);

        if (IsExchangeableTo(other)) return Math.Abs(DefaultQuantity / quantity);

        throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, name);
    }

    public bool TryExchangeTo(IBaseMeasurable context, [NotNullWhen(true)] out IBaseRate? exchanged)
    {
        exchanged = ExchangeTo(context);

        return exchanged != null;
    }

    #region Override methods
    public override bool Equals(object? obj)
    {
        return obj is IBaseRate baseRate && Equals(baseRate);
    }

    public override IBaseMeasurableFactory GetFactory()
    {
        return (IBaseRateFactory)Factory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(DefaultQuantity, MeasureUnitTypeCode, NumeratorMeasureUnitTypeCode);
    }

    public override void Validate(IFooVariaObject? fooVariaObject)
    {
        base.Validate(fooVariaObject);
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

        if (IsExchangeableTo(other)) return DefaultQuantity.CompareTo(other.DefaultQuantity);

        throw BaseRateArgumentMeasureUnitTypeCodesOutOfRangeException(other, nameof(other));
    }

    public virtual bool Equals(IBaseRate? other)
    {
        return other?.DefaultQuantity == DefaultQuantity
            && other.MeasureUnitTypeCode == MeasureUnitTypeCode
            && other.NumeratorMeasureUnitTypeCode == NumeratorMeasureUnitTypeCode;
    }

    public virtual bool IsExchangeableTo(IBaseMeasurable? baseMeasurable)
    {
        return AreExchangeables(this, baseMeasurable);
    }
    #endregion

    #region Abstract methods
    public abstract IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode);
    public abstract IBaseRate? ExchangeTo(IBaseMeasurable context);
    #endregion

    #region Static methods
    public static bool AreExchangeables(IBaseRate baseRate, IBaseMeasurable? baseMeasurable)
    {
        if (baseMeasurable is not IBaseRate other) return baseMeasurable?.IsExchangeableTo(baseRate.MeasureUnitTypeCode) == true;

        return baseRate.MeasureUnitTypeCode == other.MeasureUnitTypeCode
            && baseRate.NumeratorMeasureUnitTypeCode == other.NumeratorMeasureUnitTypeCode;
    }

    public static int Compare(IBaseRate baseRate, IBaseRate? other)
    {
        return NullChecked(baseRate, nameof(baseRate)).CompareTo(other);
    }

    public static bool Equals(IBaseRate baseRate, IBaseRate? other)
    {
        return baseRate?.Equals(other) == true;
    }

    public static decimal Proportion(IBaseRate baseRate, IBaseRate other)
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

        throw exception(name, baseRate.NumeratorMeasureUnitTypeCode);

        #region Local methods
        static ArgumentOutOfRangeException exception(string name, MeasureUnitTypeCode measureUnitTypeCode)
        {
            return new ArgumentOutOfRangeException(name, measureUnitTypeCode, null);
        }
        #endregion
    }
    #endregion
}
