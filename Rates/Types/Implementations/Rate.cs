namespace CsabaDu.FooVaria.Rates.Types.Implementations;

internal abstract class Rate : BaseRate, IRate
{
    #region Constructors
    private protected Rate(IRate other) : base(other)
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
        //DefaultQuantity = other.GetDefaultQuantity();
    }

    protected Rate(IRateFactory factory, IRate rate) : base(factory, rate)
    {
        Numerator = rate.Numerator;
        Denominator = rate.Denominator;
        //DefaultQuantity = rate.GetDefaultQuantity();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, MeasureUnitCode denominatorMeasureUnitCode) : base(factory, denominatorMeasureUnitCode)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = CreateDenominator(denominatorMeasureUnitCode);
        //DefaultQuantity = numerator.GetDefaultQuantity();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, Enum denominatorMeasureUnit, ValueType denominatorQuantity) : base(factory, denominatorMeasureUnit)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = factory.DenominatorFactory.Create(denominatorMeasureUnit, denominatorQuantity);
        //DefaultQuantity = numerator.GetDefaultQuantity() * GetExchangeRate(denominatorMeasureUnit) / Denominator.GetDefaultQuantity();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IMeasurement denominatorMeasurement) : base(factory, denominatorMeasurement)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = factory.DenominatorFactory.Create(denominatorMeasurement);
        //DefaultQuantity = numerator.GetDefaultQuantity() * denominatorMeasurement.GetExchangeRate();
    }

    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, denominator)
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = NullChecked(denominator, nameof(denominator));
        //DefaultQuantity = numerator.GetDefaultQuantity() / denominator.GetDefaultQuantity();
    }
    #endregion

    #region Properties
    public IMeasure Numerator { get; init; }
    public IDenominator Denominator { get; init; }

    #region Override properties
    #region Sealed properties
    //public override sealed decimal DefaultQuantity { get; init; }
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

        ValidateMeasureUnitCodeByDefinition(measureUnitCode, paramName);

        return Numerator.Divide(denominator.GetDefaultQuantity()); // TODO check logic!!!
    }

    public bool Equals(IRate? other)
    {
        return base.Equals(other);
    }

    public bool Equals(IRate? x, IRate? y)
    {
        if (x == null && y == null) return true;

        if (x == null || y == null) return false;

        ILimit? xLimit = x.GetLimit();

        return xLimit?.Equals(xLimit, y.GetLimit()) != false
            && x.Equals(y);
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

    public int GetHashCode([DisallowNull] IRate rate)
    {
        return HashCode.Combine(rate.GetLimit(), rate.GetHashCode());
    }

    public IRate GetRate(params IBaseMeasure[] rateComponents)
    {
        return GetFactory().Create(rateComponents);
    }

    public IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode)
    {
        IBaseMeasure? rateComponent = this[rateComponentCode];
            
        return rateComponent ?? throw InvalidRateComponentCodeArgumentException(rateComponentCode);
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

    public bool IsExchangeableTo(IMeasurable? context)
    {
        return context switch
        {
            BaseRate baseRate => base.IsExchangeableTo(baseRate),

            BaseMeasure or
            BaseMeasurement => context!.HasMeasureUnitCode(MeasureUnitCode),

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
    public override sealed decimal GetDefaultQuantity()
    {
        return Numerator.GetDefaultQuantity() / Denominator.GetDefaultQuantity();
    }

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

        return (IDenominator)(factory.CreateDefault(denominatorMeasureUnitCode)
            ?? throw InvalidMeasureUnitCodeEnumArgumentException(denominatorMeasureUnitCode, nameof(denominatorMeasureUnitCode)));
    }
    #endregion
}
