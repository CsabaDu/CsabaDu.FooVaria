namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal abstract class Rate : BaseRate, IRate
{
    #region Constructors
    private protected Rate(IRate other) : base(other)
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
        DefaultQuantity = other.DefaultQuantity;
    }

    protected Rate(IRateFactory factory, IRate baseRate) : base(factory, baseRate)
    {
        Numerator = baseRate.Numerator;
        Denominator = baseRate.Denominator;
        DefaultQuantity = baseRate.DefaultQuantity;
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode) : base(factory, denominatorMeasureUnitTypeCode)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = getDenominator();
        DefaultQuantity = numerator.DefaultQuantity;

        #region Local methods
        IDenominator getDenominator()
        {
            IDenominatorFactory denominatorFactory = factory.DenominatorFactory;

            IDenominator? denominator = denominatorFactory.CreateDefault(denominatorMeasureUnitTypeCode);

            return denominator ?? throw InvalidMeasureUnitTypeCodeEnumArgumentException(denominatorMeasureUnitTypeCode);
        }
        #endregion
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, Enum denominatorMeasureUnit) : base(factory, denominatorMeasureUnit)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = factory.DenominatorFactory.Create(denominatorMeasureUnit);
        DefaultQuantity = numerator.DefaultQuantity * GetExchangeRate(denominatorMeasureUnit);
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, denominator)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = NullChecked(denominator, nameof(denominator));
        DefaultQuantity = numerator.DefaultQuantity / denominator.DefaultQuantity;
    }
    #endregion

    #region Properties
    public IRateComponent? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => Numerator,
        RateComponentCode.Denominator => Denominator,
        RateComponentCode.Limit => GetLimit(),

        _ => null,
    };
    public IMeasure Numerator { get; init; }
    public IDenominator Denominator { get; init; }

    #region Override methods
    #region Sealed methods
    public override sealed decimal DefaultQuantity { get; init; }
    #endregion
    #endregion
    #endregion

    #region Public methods
    public int CompareTo(IRate? other)
    {
        return base.CompareTo(other);
    }

    public IMeasure Denominate(IRateComponent denominator)
    {
        string paramName = nameof(denominator);
        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(denominator, paramName).MeasureUnitTypeCode;

        ValidateMeasureUnitTypeCode(measureUnitTypeCode, paramName);

        return Numerator.Divide(denominator.DefaultQuantity); // TODO check logic!!!
    }

    public bool Equals(IRate? other)
    {
        return base.Equals(other);
    }

    public IRate? ExchangeTo(IMeasurable context)
    {
        if (context is IMeasurement measurement) return exchangeToMeasurement(measurement);

        if (context is IRateComponent rateComponent) return exchangeToRateComponent(rateComponent);

        return null;

        #region Local methods
        IRate? exchangeToMeasurement(IMeasurement? measurement)
        {
            if (measurement?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

            IDenominator denominator = Denominator.GetDenominator(measurement);
            decimal proportionQuantity = denominator.Measurement.ProportionalTo(measurement);

            return exchange(denominator, proportionQuantity);
        }

        IRate? exchangeToRateComponent(IRateComponent? rateComponent)
        {
            if (rateComponent?.IsExchangeableTo(MeasureUnitTypeCode) != true) return null;

            IDenominator denominator = Denominator.GetRateComponent(rateComponent);
            decimal proportionQuantity = denominator.ProportionalTo(rateComponent);

            return exchange(denominator, proportionQuantity);
        }

        IRate? exchange(IDenominator denominator, decimal proportionQuantity)
        {
            IMeasure numerator = Numerator.Divide(proportionQuantity);

            return GetRate(numerator, denominator, GetLimit());
        }
        #endregion
    }

    public IRate GetRate(params IRateComponent[] rateComponents)
    {
        return GetFactory().Create(rateComponents);
    }

    public IRateComponent GetRateComponent(RateComponentCode rateComponentCode)
    {
        IRateComponent? rateComponent = this[rateComponentCode];
            
        return rateComponent ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    public bool IsExchangeableTo(IMeasurable? context)
    {
        return context switch
        {
            BaseMeasurement baseMeasurement => baseMeasurement.HasMeasureUnitTypeCode(MeasureUnitTypeCode),
            BaseRate baseRate => base.IsExchangeableTo(baseRate),

            _ => false,
        };
    }

    public decimal ProportionalTo(IRate other)
    {
        return base.ProportionalTo(other);
    }

    #region Override methods
    public override IRateFactory GetFactory()
    {
        return (IRateFactory)Factory;
    }

    #region Sealed methods
    public override sealed IRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
    {
        Enum measureUnit = Defined(numeratorMeasureUnitTypeCode, nameof(numeratorMeasureUnitTypeCode)).GetDefaultMeasureUnit();

        ValidateQuantity(defaultQuantity, nameof(defaultQuantity));

        IRateComponent denominator = Denominator.GetDefault(denominatorMeasureUnitTypeCode)
            ?? throw InvalidMeasureUnitTypeCodeEnumArgumentException(denominatorMeasureUnitTypeCode, nameof(denominatorMeasureUnitTypeCode));
        IRateComponent numerator = Numerator.GetRateComponent(measureUnit, defaultQuantity);

        return GetRate(numerator, denominator);
    }

    public override sealed Enum GetMeasureUnit()
    {
        return Numerator.GetMeasureUnit();
    }

    public override sealed MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode()
    {
        return Numerator.MeasureUnitTypeCode;
    }

    public override sealed void ValidateQuantity(ValueType? quantity, string paramName)
    {
        decimal decimalQuantity = (decimal)(NullChecked(quantity, paramName).ToQuantity(TypeCode.Decimal)
            ?? throw ArgumentTypeOutOfRangeException(paramName, quantity!));

        if (decimalQuantity > 0) return;

        throw QuantityArgumentOutOfRangeException(paramName, quantity);
    }
    #endregion
    #endregion

    #region Abstract methods
    public abstract ILimit? GetLimit();
    #endregion
    #endregion
}


//{
//    #region Constructors
//    private protected Rate(IRate other) : base(other)
//    {
//        Numerator = other.Numerator;
//        Denominator = other.Denominator;
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, Enum measureUnit) : base(factory, measureUnit)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = factory.DenominatorFactory.CreateNew(measureUnit);
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitTypeCode measureUnitTypeCode) : base(factory, measureUnitTypeCode)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = (IDenominator)factory.DenominatorFactory.CreateDefault(measureUnitTypeCode);
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, IMeasurement measurement) : base(factory, measurement)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = factory.DenominatorFactory.CreateNew(measurement);
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, denominator)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = denominator;
//    }
//    #endregion

//    #region Properties
//    public IRateComponent? this[RateComponentCode rateComponentCode] => rateComponentCode switch
//    {
//        RateComponentCode.Denominator => Denominator,
//        RateComponentCode.Numerator => Numerator,
//        RateComponentCode.Limit => GetLimit(),

//        _ => null,
//    };
//    public IDenominator Denominator { get; init; }
//    public IMeasure Numerator { get; init; }
//    public decimal DefaultQuantity => Numerator.DefaultQuantity / Denominator.DefaultQuantity;
//    public MeasureUnitTypeCode GetNumeratorMeasureUnitTypeCode() => Numerator.MeasureUnitTypeCode;
//    #endregion

//    #region Public methods
//    public override TypeCode? GetQuantityTypeCode(object quantity)
//    {
//        throw new NotImplementedException();
//    }

//    public int CompareTo(IBaseRate? other)
//    {
//        return BaseRate.Compare(this, other);
//    }

//    public IBaseRate? ExchangeTo(IMeasurable denominator)
//    {
//        return denominator switch
//        {
//            //Measurement measurement => Exchange(this, measurement),
//            RateComponent rateComponent => Exchange(this, rateComponent),

//            _ => null,
//        };
//    }

//    public IBaseRate GetBaseRate(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
//    {
//        IMeasure numerator = Numerator.GetRateComponent(numeratorMeasureUnitTypeCode.GetDefaultMeasureUnit());
//        IDenominator denominator = Denominator.GetDenominator(denominatorMeasureUnitTypeCode.GetDefaultMeasureUnit(), defaultQuantity);

//        return GetRate(numerator, denominator, null);
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
//    {
//        return GetFactory().CreateNew(numerator, denominatorMeasureUnit);
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
//    {
//        return GetFactory().CreateNew(numerator, denominatorMeasureUnitTypeCode);
//    }

//    public IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
//    {
//        string name = nameof(numerator);

//        if (NullChecked(numerator, name) is not IMeasure measure)
//        {
//            throw ArgumentTypeOutOfRangeException(name, numerator);
//        }

//        name = nameof(denominator);
//        IDenominatorFactory denominatorFactory = GetFactory().DenominatorFactory;
//        IDenominator createdDenominator;

//        _ = NullChecked(denominator, name);

//        if (denominator is IMeasurement measurement)
//        {
//            createdDenominator = denominatorFactory.CreateNew(measurement);

//        }
//        else if (denominator is IRateComponent rateComponent)
//        {
//            createdDenominator = (IDenominator)denominatorFactory.CreateNew(rateComponent);
//        }
//        else
//        {
//            throw ArgumentTypeOutOfRangeException(name, denominator);
//        }

//        return GetRate(measure, createdDenominator, null);
//    }

//    public decimal GetQuantity()
//    {
//        return Numerator.GetDecimalQuantity() / Denominator.GetDecimalQuantity();
//    }

//    public IRate GetRate(IRate other)
//    {
//        return (IRate)GetFactory().CreateNew(other);
//    }

//    public IRateComponent? GetRateComponent(RateComponentCode rateComponentCode)
//    {
//        return this[Defined(rateComponentCode, nameof(rateComponentCode))];
//    }

//    public bool IsExchangeableTo(IMeasurable? baseMeasurable)
//    {
//        return BaseRate.AreExchangeables(this, baseMeasurable);
//    }

//    public IQuantifiable Denominate(IBaseMeasure multiplier)
//    {
//        MeasureUnitTypeCode measureUnitTypeCode = NullChecked(multiplier, nameof(multiplier)).MeasureUnitTypeCode;

//        ValidateMeasureUnitTypeCode(measureUnitTypeCode, nameof(multiplier));

//        if (multiplier is IMeasure measure) return Numerator.Denominate(measure.DefaultQuantity);

//        if (multiplier is IMeasurement measurement) return Numerator.ExchangeTo(measurement.GetMeasureUnit())!;

//        throw ArgumentTypeOutOfRangeException(nameof(multiplier), multiplier);
//    }

//    public decimal ProportionalTo(IBaseRate other)
//    {
//        return BaseRate.Proportionals(this, other);
//    }

//    //public bool TryExchangeTo(IMeasurable denominator, [NotNullWhen(true)] out IBaseRate? exchanged)
//    //{
//    //    exchanged = ExchangeTo(denominator);

//    //    return exchanged != null;
//    //}

//    public void ValidateQuantity(ValueType? quantity, string paramName)
//    {
//        Numerator.ValidateQuantity(quantity, paramName);
//    }

//    #region Virtual methods
//    public virtual bool Equals(IBaseRate? other)
//    {
//        return BaseRate.Equals(this, other);
//    }

//    public virtual ILimit? GetLimit()
//    {
//        return null;
//    }
//    #endregion

//    #region Override methods
//    public override IRateFactory GetFactory()
//    {
//        return (IRateFactory)Factory;
//    }

//    #region Sealed methods
//    public override sealed bool Equals(object? obj)
//    {
//        return obj is IRate other
//            && Equals(other);
//    }

//    public override sealed int GetHashCode()
//    {
//        return HashCode.Combine(Numerator, Denominator);
//    }

//    public override sealed IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
//    {
//        yield return Numerator.MeasureUnitTypeCode;
//        yield return Denominator.MeasureUnitTypeCode;

//        ILimit? limit = GetLimit();

//        if (limit == null) yield break;

//        yield return limit.MeasureUnitTypeCode;
//    }

//    public override sealed TypeCode GetQuantityTypeCode()
//    {
//        return Numerator.GetQuantityTypeCode();
//    }

//    public override sealed void Validate(IRootObject? rootObject, string paramName)
//    {
//        Validate(this, rootObject, validateRate, paramName);

//        #region Local methods
//        void validateRate()
//        {
//            base.Validate(rootObject, paramName);

//            if (rootObject is not IRate rate) throw ArgumentTypeOutOfRangeException(paramName, rootObject!);

//            Numerator.Validate(rate.Numerator, paramName);

//            if (rootObject is not ILimitedRate limitedRate) return;

//            GetLimit()?.Validate(limitedRate.Limit, paramName);
//        }
//        #endregion
//    }

//    public override sealed void ValidateMeasureUnit(Enum measureUnit, string paramName)
//    {
//        Denominator.ValidateMeasureUnit(measureUnit, paramName);
//    }
//    #endregion
//    #endregion

//    #region Abstract methods
//    public abstract IRate GetRate(IMeasure numerator, IDenominator denominator, ILimit? limit);
//    #endregion

//    #region Static methods
//    public static IRate? Exchange(IRate rate, IMeasurement? measurement)
//    {
//        if (rate == null) return null;

//        if (measurement?.IsExchangeableTo(rate.MeasureUnitTypeCode) != true) return null;

//        IDenominator denominator = rate.Denominator;
//        decimal proportionQuantity = denominator.Measurement.ProportionalTo(measurement!);
//        denominator = denominator.GetDenominator(measurement!);

//        return Exchange(rate, denominator, proportionQuantity);
//    }

//    public static IRate? Exchange(IRate rate, IRateComponent? rateComponent)
//    {
//        if (rate == null) return null;

//        if (rateComponent?.IsExchangeableTo(rate.MeasureUnitTypeCode) != true) return null;

//        IDenominator denominator = rate.Denominator;
//        decimal proportionQuantity = denominator.ProportionalTo(rateComponent!);
//        denominator = denominator.GetDenominator(rateComponent!);

//        return Exchange(rate, denominator, proportionQuantity);
//    }
//    #endregion
//    #endregion

//    #region Protected methods
//    #region Static methods
//    protected static TNum GetValidRate<TNum>(TNum commonBase, IRootObject other, string paramName) where TNum : class, IRate
//    {
//        TNum rate = GetValidMeasurable(commonBase, other, paramName);
//        MeasureUnitTypeCode measureUnitTypeCode = commonBase.Numerator.MeasureUnitTypeCode;
//        MeasureUnitTypeCode otherMeasureUnitTypeCode = rate.Numerator.MeasureUnitTypeCode;

//        return GetValidBaseMeasurable(rate, measureUnitTypeCode, otherMeasureUnitTypeCode, paramName);
//    }
//    #endregion
//    #endregion

//    #region Private methods
//    #region Static methods
//    private static IRate? Exchange(IRate rate, IDenominator denominator, decimal proportionQuantity)
//    {
//        IMeasure numerator = rate.Numerator.Divide(proportionQuantity);

//        return rate.GetRate(numerator, denominator, rate.GetLimit());
//    }

//    public decimal GetDefaultQuantity()
//    {
//        throw new NotImplementedException();
//    }

//    public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
//    {
//        throw new NotImplementedException();
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, IMeasurable denominator)
//    {
//        throw new NotImplementedException();
//    }
//    #endregion
//    #endregion
//}
