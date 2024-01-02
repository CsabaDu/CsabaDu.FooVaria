//namespace CsabaDu.FooVaria.RateComponents.Types.Implementations;

//internal abstract class Rate : Measurable, IRate
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
//            RateComponent baseMeasure => Exchange(this, baseMeasure),

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
//        else if (denominator is IRateComponent baseMeasure)
//        {
//            createdDenominator = (IDenominator)denominatorFactory.CreateNew(baseMeasure);
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
//        decimal proportion = denominator.Measurement.ProportionalTo(measurement!);
//        denominator = denominator.GetDenominator(measurement!);

//        return Exchange(rate, denominator, proportion);
//    }

//    public static IRate? Exchange(IRate rate, IRateComponent? baseMeasure)
//    {
//        if (rate == null) return null;

//        if (baseMeasure?.IsExchangeableTo(rate.MeasureUnitTypeCode) != true) return null;

//        IDenominator denominator = rate.Denominator;
//        decimal proportion = denominator.ProportionalTo(baseMeasure!);
//        denominator = denominator.GetDenominator(baseMeasure!);

//        return Exchange(rate, denominator, proportion);
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
//    private static IRate? Exchange(IRate rate, IDenominator denominator, decimal proportion)
//    {
//        IMeasure numerator = rate.Numerator.Divide(proportion);

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
