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
    #endregion

    #region Public methods
    public int CompareTo(IRate? other)
    {
        if (other == null) return 1;

        if (!IsExchangeableTo(other)) throw new ArgumentOutOfRangeException(nameof(other));

        return GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());
    }

    public virtual bool Equals(IRate? other)
    {
        return Denominator.Equals(other?.Denominator)
            && Numerator.Equals(other?.Numerator);
    }

    public IRate? ExchangeTo(IDenominator denominator)
    {
        if (denominator?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

        IMeasure numerator = Numerator.Divide(Denominator.ProportionalTo(denominator!));

        return GetRate(numerator, denominator, GetLimit());
    }

    public decimal GetDefaultQuantity()
    {
        return Numerator.DefaultQuantity / Denominator.DefaultQuantity;
    }

    public virtual ILimit? GetLimit()
    {
        return null;
    }

    public MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode()
    {
        return Numerator.MeasureUnitTypeCode;
    }

    public IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[Defined(rateComponentCode, nameof(rateComponentCode))];
    }

    public IRate GetRate(IRate other)
    {
        return (IRate)GetFactory().Create(other);
    }

    public bool IsExchangeableTo(IRate? other)
    {
        return other?.Denominator.IsExchangeableTo(MeasureUnitTypeCode) == true
            && other.Numerator.IsExchangeableTo(Numerator.MeasureUnitTypeCode);
    }

    public decimal ProportionalTo(IRate rate)
    {
        return GetDefaultQuantity() / NullChecked(rate, nameof(rate)).GetDefaultQuantity();
    }

    public bool TryExchangeTo(IDenominator denominator, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = ExchangeTo(denominator);

        return exchanged != null;
    }

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

    //public override sealed Enum GetMeasureUnit()
    //{
    //    return Denominator.GetMeasureUnit();
    //}

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
    #endregion

    #region Protected methods
    #region Static methods
    protected static T? ExchangeTo<T>(T rate, IDenominator denominator) where T : class, IRate
    {
        if (denominator?.IsExchangeableTo(rate.MeasureUnitTypeCode) != true) return null;

        IMeasure numerator = rate.Numerator.Divide(rate.Denominator.ProportionalTo(denominator!));

        return (T)rate.GetRate(numerator, denominator, rate.GetLimit());
    }

    protected static T GetValidRate<T>(T commonBase, IFooVariaObject other) where T : class, IRate
    {
        T rate = GetValidBaseMeasurable(commonBase, other);
        MeasureUnitTypeCode measureUnitTypeCode = commonBase.Numerator.MeasureUnitTypeCode;
        MeasureUnitTypeCode otherMeasureUnitTypeCode = rate.Numerator.MeasureUnitTypeCode;

        return GetValidBaseMeasurable(rate, measureUnitTypeCode, otherMeasureUnitTypeCode);
    }
    #endregion
    #endregion
}
