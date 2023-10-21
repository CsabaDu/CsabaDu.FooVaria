using CsabaDu.FooVaria.Common;

namespace CsabaDu.FooVaria.Measurables.Types.Implementations;

internal abstract class Rate : Measurable, IRate
{
    #region Constructors
    private protected Rate(IRate other) : base(other)
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
    }
    private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = (IDenominator)factory.DenominatorFactory.CreateDefault(measureUnitTypeCode);
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IMeasurement measurement) : base(factory, measurement)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = factory.DenominatorFactory.Create(measurement);
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, denominator)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = denominator;
    }
    #endregion

    #region Properties
    public IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Denominator => Denominator,
        RateComponentCode.Numerator => Numerator,
        RateComponentCode.Limit => GetLimit(),

        _ => null,
    };
    public IDenominator Denominator { get; init; }
    public IMeasure Numerator { get; init; }
    public decimal DefaultQuantity => Numerator.DefaultQuantity / Denominator.DefaultQuantity;
    public MeasureUnitTypeCode NumeratorMeasureUnitTypeCode => Numerator.MeasureUnitTypeCode;
    #endregion

    #region Public methods
    public int CompareTo(IBaseRate? other)
    {
        return BaseRate.Compare(this, other);
    }

    public IBaseRate? ExchangeTo(IBaseMeasurable context)
    {
        return context switch
        {
            Measurement measurement => Exchange(this, measurement),
            BaseMeasure baseMeasure => Exchange(this, baseMeasure),

            _ => null,
        };
    }

    public IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode)
    {
        IMeasure numerator = Numerator.GetMeasure(numeratorMeasureUnitTypeCode.GetDefaultMeasureUnit());
        IDenominator denominator = Denominator.GetDenominator(measureUnitTypeCode.GetDefaultMeasureUnit(), defaultQuantity);

        return GetRate(numerator, denominator, null);
    }

    public IRate GetRate(IRate other)
    {
        return (IRate)GetFactory().Create(other);
    }

    public IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[Defined(rateComponentCode, nameof(rateComponentCode))];
    }

    public bool IsExchangeableTo(IBaseMeasurable? baseMeasurable)
    {
        return BaseRate.AreExchangeables(this, baseMeasurable);
    }

    public decimal ProportionalTo(IBaseRate other)
    {
        return BaseRate.Proportion(this, other);
    }

    public bool TryExchangeTo(IBaseMeasurable context, [NotNullWhen(true)] out IBaseRate? exchanged)
    {
        exchanged = ExchangeTo(context);

        return exchanged != null;
    }

    #region Virtual methods
    public virtual bool Equals(IBaseRate? other)
    {
        return BaseRate.Equals(this, other);
    }

    public virtual ILimit? GetLimit()
    {
        return null;
    }
    #endregion

    #region Override methods
    public override IRateFactory GetFactory()
    {
        return (IRateFactory)Factory;
    }

    public override void Validate(IFooVariaObject? fooVariaObject)
    {
        ValidateCommonBaseAction = () => _ = GetValidRate(this, fooVariaObject!);

        Validate(this, fooVariaObject);
    }

    #region Sealed methods
    public override sealed bool Equals(object? obj)
    {
        return obj is IRate other
            && Equals(other);
    }

    public override sealed IRate GetDefault()
    {
        IMeasure numerator = (IMeasure)Numerator.GetDefault();
        IDenominator denominator = Denominator.GetDefaultRateComponent();
        ILimit? limit = GetLimit()?.GetDefaultRateComponent();

        return GetRate(numerator, denominator, limit);
    }

    public override sealed int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    public override sealed TypeCode GetQuantityTypeCode()
    {
        return Numerator.GetQuantityTypeCode();
    }

    public override sealed void ValidateMeasureUnit(Enum measureUnit)
    {
        Denominator.ValidateMeasureUnit(measureUnit);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit);
    #endregion

    #region Static methods
    public static IRate? Exchange(IRate rate, IMeasurement? measurement)
    {
        if (measurement == null) return null;

        if (measurement?.IsExchangeableTo(rate.MeasureUnitTypeCode) != true) return null;

        IDenominator denominator = rate.Denominator;
        decimal proportion = denominator.Measurement.ProportionalTo(measurement!);
        denominator = denominator.GetDenominator(measurement!);

        return Exchange(rate, denominator, proportion);
    }

    public static IRate? Exchange(IRate rate, IBaseMeasure? baseMeasure)
    {
        if (rate == null) return null;

        if (baseMeasure?.IsExchangeableTo(rate.MeasureUnitTypeCode) != true) return null;

        IDenominator denominator = rate.Denominator;
        decimal proportion = denominator.ProportionalTo(baseMeasure!);
        denominator = denominator.GetDenominator(baseMeasure!);

        return Exchange(rate, denominator, proportion);
    }
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static T GetValidRate<T>(T commonBase, IFooVariaObject other) where T : class, IRate
    {
        T rate = GetValidBaseMeasurable(commonBase, other);
        MeasureUnitTypeCode measureUnitTypeCode = commonBase.Numerator.MeasureUnitTypeCode;
        MeasureUnitTypeCode otherMeasureUnitTypeCode = rate.Numerator.MeasureUnitTypeCode;

        return GetValidBaseMeasurable(rate, measureUnitTypeCode, otherMeasureUnitTypeCode);
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static IRate? Exchange(IRate rate, IDenominator denominator, decimal proportion)
    {
        IMeasure numerator = rate.Numerator.Divide(proportion);

        return rate.GetRate(numerator, denominator, rate.GetLimit());
    }
    #endregion
    #endregion
}
