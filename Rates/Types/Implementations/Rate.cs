namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal abstract class Rate : BaseRate, IRate
{
    #region Constructors
    private protected Rate(IRate other) : base(other)
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
        DefaultQuantity = other.GetDefaultQuantity();
    }

    protected Rate(IRateFactory factory, IRate rate) : base(factory, rate)
    {
        Numerator = rate.Numerator;
        Denominator = rate.Denominator;
        DefaultQuantity = rate.GetDefaultQuantity();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode) : base(factory, denominatorMeasureUnitCode)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = CreateDenominator(denominatorMeasureUnitCode);
        DefaultQuantity = numerator.GetDefaultQuantity();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity) : base(factory, denominatorMeasureUnit)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = factory.DenominatorFactory.Create(denominatorMeasureUnit, denominatorQuantity);
        DefaultQuantity = numerator.GetDefaultQuantity() * GetExchangeRate(denominatorMeasureUnit) / Denominator.GetDefaultQuantity();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IMeasurement denominatorMeasurement) : base(factory, denominatorMeasurement)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = factory.DenominatorFactory.Create(denominatorMeasurement);
        DefaultQuantity = numerator.GetDefaultQuantity() * denominatorMeasurement.GetExchangeRate();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, denominator)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = NullChecked(denominator, nameof(denominator));
        DefaultQuantity = numerator.GetDefaultQuantity() / denominator.GetDefaultQuantity();
    }
    #endregion

    #region Properties
    public IMeasure Numerator { get; init; }
    public IDenominator Denominator { get; init; }

    #region Override properties
    #region Sealed properties
    public override sealed decimal DefaultQuantity { get; init; }
    #endregion
    #endregion

    #region New properties
    public new IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => Numerator,
        RateComponentCode.Denominator => Denominator,
        RateComponentCode.Limit => GetLimit(),

        _ => null,
    };
    #endregion
    #endregion

    #region Public methods
    public int CompareTo(IRate? other)
    {
        return base.CompareTo(other);
    }

    public IMeasure Denominate(IBaseMeasure denominator)
    {
        string paramName = nameof(denominator);
        MeasureUnitCode measureUnitCode = NullChecked(denominator, paramName).MeasureUnitCode;

        ValidateMeasureUnitCode(measureUnitCode, paramName);

        return Numerator.Divide(denominator.GetDefaultQuantity()); // TODO check logic!!!
    }

    public bool Equals(IRate? other)
    {
        return base.Equals(other);
    }

    public IRate? ExchangeTo(IMeasurable context)
    {
        if (context is IMeasurement measurement) return exchangeToMeasurement(measurement);

        if (context is IBaseMeasure rateComponent) return exchangeToRateComponent(rateComponent);

        return null;

        #region Local methods
        IRate? exchangeToMeasurement(IMeasurement? measurement)
        {
            if (measurement?.IsExchangeableTo(MeasureUnitCode) != true) return null;

            IDenominator denominator = Denominator.GetDenominator(measurement);
            decimal proportionQuantity = denominator.Measurement.ProportionalTo(measurement);

            return exchange(denominator, proportionQuantity);
        }

        IRate? exchangeToRateComponent(IBaseMeasure? baseMeasure)
        {
            if (baseMeasure?.IsExchangeableTo(MeasureUnitCode) != true) return null;

            IDenominator denominator = Denominator.GetBaseMeasure(baseMeasure);
            decimal proportionQuantity = denominator.ProportionalTo(baseMeasure);

            return exchange(denominator, proportionQuantity);
        }

        IRate? exchange(IDenominator denominator, decimal proportionQuantity)
        {
            IMeasure numerator = Numerator.Divide(proportionQuantity);
            ILimit? limit = GetLimit();

            return limit == null ?
                GetRate(numerator, denominator)
                : GetRate(numerator, denominator, limit);
        }
        #endregion
    }

    public IRate GetRate(params IBaseMeasure[] rateComponents)
    {
        return GetFactory().Create(rateComponents);
    }

    public IRate GetRate(IBaseRate baseRate)
    {
        decimal defaultQuantity = NullChecked(baseRate, nameof(baseRate)).GetDefaultQuantity();
        MeasureUnitCode numeratorMeasureUnitCode = getMeasureUnitCode(RateComponentCode.Numerator);
        MeasureUnitCode denominatorMeasureUnitCode = getMeasureUnitCode(RateComponentCode.Denominator);

        return GetBaseRate(numeratorMeasureUnitCode, defaultQuantity, denominatorMeasureUnitCode);

        #region Local methods
        MeasureUnitCode getMeasureUnitCode(RateComponentCode rateComponentCode)
        {
            return baseRate[rateComponentCode]!.Value;
        }
        #endregion
    }

    public IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode)
    {
        IBaseMeasure? rateComponent = this[rateComponentCode];
            
        return rateComponent ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
    }

    public bool IsExchangeableTo(IMeasurable? context)
    {
        return context switch
        {
            BaseRate baseRate => base.IsExchangeableTo(baseRate),
            BaseMeasure => context is IBaseMeasure && isExchangeable(),
            BaseMeasurement => isExchangeable(),

           _ => false,
        };

        #region Local methods
        bool isExchangeable()
        {
            return context!.HasMeasureUnitCode(MeasureUnitCode);
        }
        #endregion
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

    public override bool Equals(object? obj)
    {
        return obj is IRate other
            && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    #region Sealed methods
    public override sealed IRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode)
    {
        Enum numeratorMeasureUnit = numeratorMeasureUnitCode.GetDefaultMeasureUnit();

        ValidateQuantity(defaultQuantity, nameof(defaultQuantity));

        IBaseMeasure denominator = CreateDenominator(denominatorMeasureUnitCode);
        IBaseMeasure numerator = Numerator.GetBaseMeasure(numeratorMeasureUnit, defaultQuantity);

        return GetRate(numerator, denominator);
    }

    public override sealed Enum GetMeasureUnit()
    {
        return Numerator.GetMeasureUnit();
    }

    public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        IEnumerable<MeasureUnitCode> measureUnitCodes = base.GetMeasureUnitCodes();
        ILimit? limit = GetLimit();

        return limit == null ?
            measureUnitCodes
            : measureUnitCodes.Append(limit.MeasureUnitCode);
    }

    public override sealed MeasureUnitCode GetNumeratorMeasureUnitCode()
    {
        return Numerator.MeasureUnitCode;
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

    #region Private methods
    private IDenominator CreateDenominator(MeasureUnitCode denominatorMeasureUnitCode)
    {
        IDenominatorFactory factory = GetFactory().DenominatorFactory;

        return factory.CreateDefault(denominatorMeasureUnitCode)
            ?? throw InvalidMeasureUnitCodeEnumArgumentException(denominatorMeasureUnitCode, nameof(denominatorMeasureUnitCode));
    }

    public bool Equals(IRate? x, IRate? y)
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] IRate obj)
    {
        throw new NotImplementedException();
    }
    #endregion
}


//{
//    #region Constructors
//    private protected Rate(IRate other) : base(other)
//    {
//        Numerator = other.Numerator;
//        Denominator = other.Denominator;
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, Enum numeratorMeasureUnit) : base(factory, numeratorMeasureUnit)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = factory.DenominatorFactory.CreateNew(numeratorMeasureUnit);
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitCode measureUnitCode) : base(factory, measureUnitCode)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = (IDenominator)factory.DenominatorFactory.CreateDefault(measureUnitCode);
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, IMeasurement measurement) : base(factory, measurement)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = factory.DenominatorFactory.CreateNew(measurement);
//    }

//    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator baseMeasure) : base(factory, baseMeasure)
//    {
//        Numerator = NullChecked(numerator, nameof(numerator));
//        Denominator = baseMeasure;
//    }
//    #endregion

//    #region Properties
//    public IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
//    {
//        RateComponentCode.Denominator => Denominator,
//        RateComponentCode.Numerator => Numerator,
//        RateComponentCode.Limit => GetLimit(),

//        _ => null,
//    };
//    public IDenominator Denominator { get; init; }
//    public IMeasure Numerator { get; init; }
//    public decimal DefaultQuantity => Numerator.DefaultQuantity / Denominator.DefaultQuantity;
//    public MeasureUnitCode GetNumeratorMeasureUnitCode() => Numerator.MeasureUnitCode;
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

//    public IBaseRate? ExchangeTo(IMeasurable baseMeasure)
//    {
//        return baseMeasure switch
//        {
//            //Measurement measurement => Exchange(this, measurement),
//            RateComponent baseMeasure => Exchange(this, baseMeasure),

//            _ => null,
//        };
//    }

//    public IBaseRate GetBaseRate(MeasureUnitCode numeratorMeasureUnitCode, decimal defaultQuantity, MeasureUnitCode denominatorMeasureUnitCode)
//    {
//        IMeasure numerator = Numerator.GetBaseMeasure(numeratorMeasureUnitCode.GetDefaultMeasureUnit());
//        IDenominator baseMeasure = Denominator.GetDenominator(denominatorMeasureUnitCode.GetDefaultMeasureUnit(), defaultQuantity);

//        return GetRate(numerator, baseMeasure, null);
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, Enum denominatorMeasureUnit)
//    {
//        return GetFactory().CreateNew(numerator, denominatorMeasureUnit);
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode)
//    {
//        return GetFactory().CreateNew(numerator, denominatorMeasureUnitCode);
//    }

//    public IBaseRate GetBaseRate(IQuantifiable numerator, IMeasurable baseMeasure)
//    {
//        string name = nameof(numerator);

//        if (NullChecked(numerator, name) is not IMeasure measure)
//        {
//            throw ArgumentTypeOutOfRangeException(name, numerator);
//        }

//        name = nameof(baseMeasure);
//        IDenominatorFactory denominatorFactory = GetFactory().DenominatorFactory;
//        IDenominator createdDenominator;

//        _ = NullChecked(baseMeasure, name);

//        if (baseMeasure is IMeasurement measurement)
//        {
//            createdDenominator = denominatorFactory.CreateNew(measurement);

//        }
//        else if (baseMeasure is IBaseMeasure baseMeasure)
//        {
//            createdDenominator = (IDenominator)denominatorFactory.CreateNew(baseMeasure);
//        }
//        else
//        {
//            throw ArgumentTypeOutOfRangeException(name, baseMeasure);
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

//    public IBaseMeasure? GetBaseMeasure(RateComponentCode rateComponentCode)
//    {
//        return this[Defined(rateComponentCode, nameof(rateComponentCode))];
//    }

//    public bool IsExchangeableTo(IMeasurable? baseMeasurable)
//    {
//        return BaseRate.AreExchangeables(this, baseMeasurable);
//    }

//    public IQuantifiable Denominate(IBaseMeasure multiplier)
//    {
//        MeasureUnitCode measureUnitCode = NullChecked(multiplier, nameof(multiplier)).MeasureUnitCode;

//        ValidateMeasureUnitCode(measureUnitCode, nameof(multiplier));

//        if (multiplier is IMeasure measure) return Numerator.Denominate(measure.DefaultQuantity);

//        if (multiplier is IMeasurement measurement) return Numerator.ExchangeTo(measurement.GetMeasureUnit())!;

//        throw ArgumentTypeOutOfRangeException(nameof(multiplier), multiplier);
//    }

//    public decimal ProportionalTo(IBaseRate other)
//    {
//        return BaseRate.Proportionals(this, other);
//    }

//    //public bool TryExchangeTo(IMeasurable baseMeasure, [NotNullWhen(true)] out IBaseRate? exchanged)
//    //{
//    //    exchanged = ExchangeTo(baseMeasure);

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

//    public override sealed IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
//    {
//        yield return Numerator.MeasureUnitCode;
//        yield return Denominator.MeasureUnitCode;

//        ILimit? limit = GetLimit();

//        if (limit == null) yield break;

//        yield return limit.MeasureUnitCode;
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

//    public override sealed void ValidateMeasureUnit(Enum numeratorMeasureUnit, string paramName)
//    {
//        Denominator.ValidateMeasureUnit(numeratorMeasureUnit, paramName);
//    }
//    #endregion
//    #endregion

//    #region Abstract methods
//    public abstract IRate GetRate(IMeasure numerator, IDenominator baseMeasure, ILimit? limit);
//    #endregion

//    #region Static methods
//    public static IRate? Exchange(IRate rate, IMeasurement? measurement)
//    {
//        if (rate == null) return null;

//        if (measurement?.IsExchangeableTo(rate.MeasureUnitCode) != true) return null;

//        IDenominator baseMeasure = rate.Denominator;
//        decimal proportionQuantity = baseMeasure.Measurement.ProportionalTo(measurement!);
//        baseMeasure = baseMeasure.GetDenominator(measurement!);

//        return Exchange(rate, baseMeasure, proportionQuantity);
//    }

//    public static IRate? Exchange(IRate rate, IBaseMeasure? baseMeasure)
//    {
//        if (rate == null) return null;

//        if (baseMeasure?.IsExchangeableTo(rate.MeasureUnitCode) != true) return null;

//        IDenominator baseMeasure = rate.Denominator;
//        decimal proportionQuantity = baseMeasure.ProportionalTo(baseMeasure!);
//        baseMeasure = baseMeasure.GetDenominator(baseMeasure!);

//        return Exchange(rate, baseMeasure, proportionQuantity);
//    }
//    #endregion
//    #endregion

//    #region Protected methods
//    #region Static methods
//    protected static TNum GetValidRate<TNum>(TNum commonBase, IRootObject other, string paramName) where TNum : class, IRate
//    {
//        TNum rate = GetValidMeasurable(commonBase, other, paramName);
//        MeasureUnitCode measureUnitCode = commonBase.Numerator.MeasureUnitCode;
//        MeasureUnitCode otherMeasureUnitCode = rate.Numerator.MeasureUnitCode;

//        return GetValidBaseMeasurable(rate, measureUnitCode, otherMeasureUnitCode, paramName);
//    }
//    #endregion
//    #endregion

//    #region Private methods
//    #region Static methods
//    private static IRate? Exchange(IRate rate, IDenominator baseMeasure, decimal proportionQuantity)
//    {
//        IMeasure numerator = rate.Numerator.Divide(proportionQuantity);

//        return rate.GetRate(numerator, baseMeasure, rate.GetLimit());
//    }

//    public decimal GetDefaultQuantity()
//    {
//        throw new NotImplementedException();
//    }

//    public void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName)
//    {
//        throw new NotImplementedException();
//    }

//    public IBaseRate GetBaseRate(IBaseMeasure numerator, IMeasurable baseMeasure)
//    {
//        throw new NotImplementedException();
//    }
//    #endregion
//    #endregion
//}
