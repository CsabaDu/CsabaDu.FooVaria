namespace CsabaDu.FooVaria.Rates.Types.Implementations;

/// <summary>
/// Represents an abstract base class for different types of rates.
/// </summary>
internal abstract class Rate : BaseRate, IRate
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Rate"/> class by copying another rate.
    /// </summary>
    /// <param name="other">The rate to copy.</param>
    private protected Rate(IRate other) : base(other, nameof(other))
    {
        Numerator = other.Numerator;
        Denominator = other.Denominator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rate"/> class using a factory and another rate.
    /// </summary>
    /// <param name="factory">The factory to create the rate.</param>
    /// <param name="rate">The rate to use for initialization.</param>
    protected Rate(IRateFactory factory, IRate rate) : base(factory, nameof(factory))
    {
        Numerator = NullChecked(rate, nameof(rate)).Numerator;
        Denominator = rate.Denominator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Rate"/> class using a factory, numerator, and denominator.
    /// </summary>
    /// <param name="factory">The factory to create the rate.</param>
    /// <param name="numerator">The numerator of the rate.</param>
    /// <param name="denominator">The denominator of the rate.</param>
    private protected Rate(IRateFactory factory, IMeasure numerator, IDenominator denominator) : base(factory, nameof(factory))
    {
        Numerator = NullChecked(numerator, nameof(numerator));
        Denominator = NullChecked(denominator, nameof(denominator));
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the numerator of the rate.
    /// </summary>
    public IMeasure Numerator { get; init; }

    /// <summary>
    /// Gets the denominator of the rate.
    /// </summary>
    public IDenominator Denominator { get; init; }

    /// <summary>
    /// Gets the base measure for the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The base measure for the specified rate component code.</returns>
    public IBaseMeasure? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => Numerator,
        RateComponentCode.Denominator => Denominator,
        RateComponentCode.Limit => GetLimit(),

        _ => null,
    };
    #endregion

    #region Public methods
    /// <summary>
    /// Compares the current rate to another rate.
    /// </summary>
    /// <param name="other">The rate to compare to.</param>
    /// <returns>An integer that indicates the relative order of the rates being compared.</returns>
    public int CompareTo(IRate? other)
    {
        return base.CompareTo(other);
    }

    /// <summary>
    /// Denominates the specified measurable object.
    /// </summary>
    /// <param name="denominator">The measurable object to denominate.</param>
    /// <returns>The resulting measure after denomination.</returns>
    public IMeasure Denominate(IMeasurable denominator)
    {
        ValidateDenominator(denominator, nameof(denominator));

        decimal divisor = denominator switch
        {
            BaseMeasurement baseMeasurement => Denominator.Measurement.ProportionalTo(baseMeasurement) * Denominator.GetDefaultQuantity(),
            Quantifiable quantifiable => Denominator.ProportionalTo(quantifiable),

            _ => throw new InvalidOperationException(null),
        };

        return Numerator.Divide(divisor);
    }

    /// <summary>
    /// Determines whether the current rate is equal to another rate.
    /// </summary>
    /// <param name="other">The rate to compare to.</param>
    /// <returns>true if the rates are equal; otherwise, false.</returns>
    public bool Equals(IRate? other)
    {
        return base.Equals(other);
    }

    /// <summary>
    /// Determines whether two rates are equal.
    /// </summary>
    /// <param name="x">The first rate to compare.</param>
    /// <param name="y">The second rate to compare.</param>
    /// <returns>true if the rates are equal; otherwise, false.</returns>
    public bool Equals(IRate? x, IRate? y)
    {
        return x is null == y is null
            && x?.GetLimit() is null == y?.GetLimit() is null
            && x?.Equals(y) != false
            && x?.Numerator.Equals(x.GetLimit(), y?.GetLimit()) != false;
    }

    /// <summary>
    /// Returns a hash code for the specified rate.
    /// </summary>
    /// <param name="rate">The rate to get the hash code for.</param>
    /// <returns>A hash code for the specified rate.</returns>
    public int GetHashCode([DisallowNull] IRate rate)
    {
        return HashCode.Combine(rate.GetHashCode(), rate.GetLimit()?.GetHashCode());
    }

    /// <summary>
    /// Gets a new rate based on the specified rate components.
    /// </summary>
    /// <param name="rateComponents">The rate components to use.</param>
    /// <returns>A new rate based on the specified rate components.</returns>
    public IRate GetRate(params IBaseMeasure[] rateComponents)
    {
        IRateFactory factory = (IRateFactory)GetFactory();

        return factory.Create(rateComponents);
    }

    /// <summary>
    /// Gets the base measure for the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The base measure for the specified rate component code.</returns>
    public IBaseMeasure GetBaseMeasure(RateComponentCode rateComponentCode)
    {
        return GetRateComponent(rateComponentCode) ?? throw InvalidRateComponentCodeEnumArgumentException(rateComponentCode);
    }

    /// <summary>
    /// Determines whether the rate is exchangeable to the specified context.
    /// </summary>
    /// <param name="context">The context to check.</param>
    /// <returns>true if the rate is exchangeable to the context; otherwise, false.</returns>
    public bool IsExchangeableTo(Enum? context)
    {
        return Denominator.IsExchangeableTo(context);
    }

    /// <summary>
    /// Determines whether the rate is exchangeable to the specified base measure.
    /// </summary>
    /// <param name="baseMeasure">The base measure to check.</param>
    /// <returns>true if the rate is exchangeable to the base measure; otherwise, false.</returns>
    public bool IsExchangeableTo(IBaseMeasure? baseMeasure)
    {
        return baseMeasure?.IsExchangeableTo(GetDenominatorCode()) == true;
    }

    /// <summary>
    /// Gets the proportional value of the current rate to another rate.
    /// </summary>
    /// <param name="other">The rate to compare to.</param>
    /// <returns>The proportional value of the current rate to the other rate.</returns>
    public decimal ProportionalTo(IRate? other)
    {
        return base.ProportionalTo(other);
    }

    /// <summary>
    /// Tries to exchange the rate to the specified context.
    /// </summary>
    /// <param name="context">The context to exchange to.</param>
    /// <param name="exchanged">The exchanged rate if successful.</param>
    /// <returns>true if the exchange was successful; otherwise, false.</returns>
    public bool TryExchangeTo(Enum? context, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(context)) return false;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));

        foreach (var item in GetRateComponentCodes())
        {
            IBaseMeasure? rateComponent = GetRateComponent(item);

            if (rateComponent?.GetMeasureUnitCode() == measureUnitElements.MeasureUnitCode
                && rateComponent!.TryExchangeTo(measureUnitElements.MeasureUnit, out IQuantifiable? exchangedRateComponent))
            {
                exchanged = item switch
                {
                    RateComponentCode.Denominator => GetRate(Numerator, (IDenominator)exchangedRateComponent!, GetLimit()!),
                    RateComponentCode.Numerator => GetRate((IMeasure)exchangedRateComponent!, Denominator, GetLimit()!),
                    RateComponentCode.Limit => GetRate(Numerator, Denominator, (ILimit)exchangedRateComponent!),

                    _ => null,
                };
            }
        }

        return exchanged is not null;
    }

    /// <summary>
    /// Tries to exchange the rate to the specified base measure.
    /// </summary>
    /// <param name="baseMeasure">The base measure to exchange to.</param>
    /// <param name="exchanged">The exchanged rate if successful.</param>
    /// <returns>true if the exchange was successful; otherwise, false.</returns>
    public bool TryExchangeTo(IBaseMeasure? baseMeasure, [NotNullWhen(true)] out IRate? exchanged)
    {
        exchanged = null;

        if (!IsExchangeableTo(baseMeasure)) return false;

        IDenominator denominator = Denominator.GetBaseMeasure(baseMeasure!);
        decimal proportionQuantity = denominator.ProportionalTo(baseMeasure);

        IMeasure numerator = Numerator.Divide(proportionQuantity);
        ILimit? limit = GetLimit();

        exchanged = GetRate(numerator, denominator, limit!);

        return exchanged is not null;
    }

    /// <summary>
    /// Validates the specified denominator.
    /// </summary>
    /// <param name="denominator">The denominator to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public void ValidateDenominator(IMeasurable denominator, string paramName)
    {
        MeasureUnitCode denominatorCode = NullChecked(denominator, paramName).GetMeasureUnitCode();

        if (denominator is IQuantifiable or IBaseMeasurement)
        {
            if (IsExchangeableTo(denominatorCode)) return;

            throw InvalidMeasureUnitCodeEnumArgumentException(denominatorCode, paramName);
        }

        throw ArgumentTypeOutOfRangeException(paramName, denominator);
    }

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Gets the default quantity of the rate.
    /// </summary>
    /// <returns>The default quantity of the rate.</returns>
    public override sealed decimal GetDefaultQuantity()
    {
        return Numerator.GetDefaultQuantity() / Denominator.GetDefaultQuantity();
    }

    /// <summary>
    /// Gets the base measure unit of the rate.
    /// </summary>
    /// <returns>The base measure unit of the rate.</returns>
    public override sealed Enum GetBaseMeasureUnit()
    {
        return Numerator.GetBaseMeasureUnit();
    }

    /// <summary>
    /// Gets the rate component for the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The rate component for the specified rate component code.</returns>
    public override sealed IBaseMeasure? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode];
    }
    #endregion
    #endregion

    #region Abstract methods
    /// <summary>
    /// Gets the limit of the rate.
    /// </summary>
    /// <returns>The limit of the rate.</returns>
    public abstract ILimit? GetLimit();

    /// <summary>
    /// Gets a new rate based on the specified rate.
    /// </summary>
    /// <param name="rate">The rate to use.</param>
    /// <returns>A new rate based on the specified rate.</returns>
    public abstract IRate GetRate(IRate rate);
    #endregion
    #endregion
}
