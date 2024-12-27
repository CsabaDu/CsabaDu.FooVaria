namespace CsabaDu.FooVaria.SimpleRates.Types.Implementations;

/// <summary>
/// Represents a simple rate with a numerator and denominator.
/// </summary>
public abstract class SimpleRate : BaseRate, ISimpleRate
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleRate"/> class using a factory and base rate.
    /// </summary>
    /// <param name="factory">The factory to create the rate.</param>
    /// <param name="baseRate">The base rate to initialize the simple rate.</param>
    protected SimpleRate(ISimpleRateFactory factory, IBaseRate baseRate) : base(factory, nameof(factory))
    {
        NumeratorCode = NullChecked(baseRate, nameof(baseRate)).GetNumeratorCode();
        DenominatorCode = baseRate.GetDenominatorCode();
        DefaultQuantity = baseRate.GetDefaultQuantity();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleRate"/> class using specific parameters.
    /// </summary>
    /// <param name="factory">The factory to create the rate.</param>
    /// <param name="numeratorCode">The numerator code of the rate.</param>
    /// <param name="defaultQuantity">The default quantity of the rate.</param>
    /// <param name="denominatorCode">The denominator code of the rate.</param>
    protected SimpleRate(ISimpleRateFactory factory, MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode) : base(factory, nameof(factory))
    {
        NumeratorCode = Defined(numeratorCode, nameof(numeratorCode));
        DenominatorCode = Defined(denominatorCode, nameof(denominatorCode));
        DefaultQuantity = GetValidPositiveQuantity(defaultQuantity, nameof(defaultQuantity));
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the numerator code of the rate.
    /// </summary>
    public MeasureUnitCode NumeratorCode { get; init; }

    /// <summary>
    /// Gets the denominator code of the rate.
    /// </summary>
    public MeasureUnitCode DenominatorCode { get; init; }

    /// <summary>
    /// Gets the default quantity of the rate.
    /// </summary>
    public decimal DefaultQuantity { get; init; }

    /// <summary>
    /// Gets the rate component based on the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The rate component.</returns>
    public Enum? this[RateComponentCode rateComponentCode] => rateComponentCode switch
    {
        RateComponentCode.Numerator => NumeratorCode,
        RateComponentCode.Denominator => DenominatorCode,
        RateComponentCode.Limit => GetLimitMode(),

        _ => null,
    };
    #endregion

    #region Public methods
    /// <summary>
    /// Denominates the rate using the specified denominator.
    /// </summary>
    /// <param name="denominator">The denominator to use.</param>
    /// <returns>The measure created using the denominator.</returns>
    public IMeasure Denominate(Enum denominator)
    {
        MeasurementElements measurementElements = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, nameof(denominator));

        decimal quantity = DefaultQuantity * measurementElements.ExchangeRate;

        return GetMeasureFactory().Create(denominator, quantity);
    }

    /// <summary>
    /// Gets the measure factory associated with the rate.
    /// </summary>
    /// <returns>The measure factory.</returns>
    public IMeasureFactory GetMeasureFactory()
    {
        return GetSimpleRateFactory().MeasureFactory;
    }

    /// <summary>
    /// Creates a new simple rate using the specified parameters.
    /// </summary>
    /// <param name="numeratorCode">The numerator code of the new rate.</param>
    /// <param name="quantity">The quantity of the new rate.</param>
    /// <param name="denominatorCode">The denominator code of the new rate.</param>
    /// <returns>The created simple rate.</returns>
    public ISimpleRate GetSimpleRate(MeasureUnitCode numeratorCode, decimal quantity, MeasureUnitCode denominatorCode)
    {
        return GetSimpleRateFactory().CreateSimpleRate(numeratorCode, quantity, denominatorCode);
    }

    /// <summary>
    /// Gets the quantity based on the specified context.
    /// </summary>
    /// <param name="context">The context to use.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The quantity.</returns>
    public decimal GetQuantity(Enum context, string paramName)
    {
        MeasurementElements measurementElements = GetMeasurementElements(context, paramName);
        MeasureUnitCode measureUnitCode = measurementElements.MeasureUnitCode;
        decimal exchangeRate = measurementElements.ExchangeRate;

        if (measureUnitCode == DenominatorCode) return DefaultQuantity * exchangeRate;

        if (measureUnitCode == NumeratorCode) return DefaultQuantity / exchangeRate;

        throw new InvalidEnumArgumentException(paramName, (int)(object)context, context.GetType());
    }

    /// <summary>
    /// Gets the quantity based on the specified numerator and denominator.
    /// </summary>
    /// <param name="numerator">The numerator to use.</param>
    /// <param name="numeratorName">The name of the numerator parameter.</param>
    /// <param name="denominator">The denominator to use.</param>
    /// <param name="denominatorName">The name of the denominator parameter.</param>
    /// <returns>The quantity.</returns>
    public decimal GetQuantity(Enum numerator, string numeratorName, Enum denominator, string denominatorName)
    {
        MeasurementElements measurementElements = GetValidMeasurementElements(numerator, RateComponentCode.Numerator, numeratorName);
        decimal numeratorExchangeRate = measurementElements.ExchangeRate;
        measurementElements = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, denominatorName);
        decimal denominatorExchangeRate = measurementElements.ExchangeRate;

        if (numeratorExchangeRate == denominatorExchangeRate) return DefaultQuantity;

        return DefaultQuantity / numeratorExchangeRate * denominatorExchangeRate;
    }

    /// <summary>
    /// Validates the specified denominator.
    /// </summary>
    /// <param name="denominator">The denominator to validate.</param>
    /// <param name="paramName">The name of the parameter.</param>
    public void ValidateDenominator(Enum denominator, string paramName)
    {
        _ = GetValidMeasurementElements(denominator, RateComponentCode.Denominator, paramName);
    }

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Gets the default quantity of the rate.
    /// </summary>
    /// <returns>The default quantity.</returns>
    public override sealed decimal GetDefaultQuantity()
    {
        return DefaultQuantity;
    }

    /// <summary>
    /// Gets the base measure unit of the rate.
    /// </summary>
    /// <returns>The base measure unit.</returns>
    public override sealed Enum GetBaseMeasureUnit()
    {
        return NumeratorCode.GetDefaultMeasureUnit()!;
    }

    /// <summary>
    /// Gets the rate component based on the specified rate component code.
    /// </summary>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <returns>The rate component.</returns>
    public override sealed Enum? GetRateComponent(RateComponentCode rateComponentCode)
    {
        return this[rateComponentCode];
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    /// <summary>
    /// Gets the simple rate factory associated with the rate.
    /// </summary>
    /// <returns>The simple rate factory.</returns>
    private ISimpleRateFactory GetSimpleRateFactory()
    {
        return (ISimpleRateFactory)GetFactory();
    }

    /// <summary>
    /// Gets the valid measurement elements based on the specified context and rate component code.
    /// </summary>
    /// <param name="context">The context to use.</param>
    /// <param name="rateComponentCode">The rate component code.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The valid measurement elements.</returns>
    private MeasurementElements GetValidMeasurementElements(Enum context, RateComponentCode rateComponentCode, string paramName)
    {
        MeasurementElements measurementElements = GetMeasurementElements(context, paramName);
        MeasureUnitCode measureUnitCode = GetMeasureUnitCode(rateComponentCode);

        if (measureUnitCode == measurementElements.MeasureUnitCode) return measurementElements;

        throw new InvalidEnumArgumentException(paramName, (int)(object)context, context.GetType());
    }
    #endregion
}
