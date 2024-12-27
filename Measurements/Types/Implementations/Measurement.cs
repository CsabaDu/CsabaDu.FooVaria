namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal abstract class Measurement : BaseMeasurement, IMeasurement
{
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="Measurement"/> class.
    /// </summary>
    /// <param name="factory">The factory used to create measurements.</param>
    /// <param name="measureUnit">The unit of measurement.</param>
    private protected Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, nameof(factory))
    {
        ValidateMeasureUnit(measureUnit, nameof(measureUnit));

        MeasureUnit = measureUnit;
        Factory = factory;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the factory used to create measurements.
    /// </summary>
    public IMeasurementFactory Factory { get; init; }

    /// <summary>
    /// Gets the unit of measurement.
    /// </summary>
    public object MeasureUnit { get; init; }
    #endregion

    #region Public methods

    #region Override methods
    #region Sealed methods
    /// <summary>
    /// Gets the base measure unit.
    /// </summary>
    /// <returns>The base measure unit.</returns>
    public override sealed Enum GetBaseMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    /// <summary>
    /// Gets the factory used to create measurements.
    /// </summary>
    /// <returns>The measurement factory.</returns>
    public override sealed IMeasurementFactory GetFactory()
    {
        return Factory;
    }

    //public override sealed RateComponentCode GetMeasureUnitCode()
    //{
    //    Enum measuureUnit = GetBaseMeasureUnitReturnValue();

    //    return GetMeasureUnitCode(measuureUnit);
    //}

    /// <summary>
    /// Gets the name of the measurement.
    /// </summary>
    /// <returns>The name of the measurement.</returns>
    public override sealed string GetName()
    {
        return GetCustomName() ?? GetDefaultName();
    }
    #endregion
    #endregion

    /// <summary>
    /// Gets the custom name of the measurement.
    /// </summary>
    /// <returns>The custom name if found; otherwise, null.</returns>
    public string? GetCustomName()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetCustomName(measureUnit);
    }

    /// <summary>
    /// Gets the collection of custom names.
    /// </summary>
    /// <returns>A dictionary of custom names.</returns>
    public IDictionary<object, string> GetCustomNameCollection()
    {
        return GetCustomNameCollection(GetMeasureUnitCode());
    }

    /// <summary>
    /// Gets the default measurement.
    /// </summary>
    /// <returns>The default measurement.</returns>
    public IMeasurement GetDefault()
    {
        return (IMeasurement)GetDefault(GetMeasureUnitCode())!;
    }

    /// <summary>
    /// Gets the default measurable object for the specified measure unit code.
    /// </summary>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <returns>The default measurable object.</returns>
    public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
    {
        return Factory.CreateDefault(measureUnitCode);
    }

    /// <summary>
    /// Gets the default name of the measurement.
    /// </summary>
    /// <returns>The default name of the measurement.</returns>
    public string GetDefaultName()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetDefaultName(measureUnit);
    }

    /// <summary>
    /// Gets a measurement for the specified measure unit.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <returns>The measurement.</returns>
    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return Factory.Create(measureUnit);
    }

    /// <summary>
    /// Gets a measurement for the specified measurement.
    /// </summary>
    /// <param name="other">The other measurement.</param>
    /// <returns>The measurement.</returns>
    public IMeasurement GetMeasurement(IMeasurement other)
    {
        return Factory.CreateNew(other);
    }

    /// <summary>
    /// Gets a measurement for the specified name.
    /// </summary>
    /// <param name="name">The name of the measurement.</param>
    /// <returns>The measurement.</returns>
    public IMeasurement GetMeasurement(string name)
    {
        return Factory.Create(name);
    }

    /// <summary>
    /// Gets a measurement for the specified custom name, measure unit code, and exchange rate.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <param name="measureUnitCode">The measure unit code.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <returns>The measurement if found; otherwise, null.</returns>
    public IMeasurement? GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        return Factory.Create(customName, measureUnitCode, exchangeRate);
    }

    /// <summary>
    /// Gets a measurement for the specified measure unit, exchange rate, and custom name.
    /// </summary>
    /// <param name="measureUnit">The measure unit.</param>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="customName">The custom name.</param>
    /// <returns>The measurement if found; otherwise, null.</returns>
    public IMeasurement? GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return Factory.Create(measureUnit, exchangeRate, customName);
    }

    /// <summary>
    /// Sets a custom name for the measurement.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    public void SetCustomName(string customName)
    {
        SetCustomName(GetBaseMeasureUnit(), customName);
    }

    /// <summary>
    /// Sets or replaces a custom name for the measurement.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <exception cref="InvalidOperationException">Thrown when the custom name cannot be set or replaced.</exception>
    public void SetOrReplaceCustomName(string customName)
    {
        ValidateCustomName(customName);

        Enum measureUnit = GetBaseMeasureUnit();

        if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

        if (CustomNameCollection.Remove(measureUnit)
            && CustomNameCollection.TryAdd(measureUnit, customName))
        {
            return;
        }

        throw new InvalidOperationException(null);
    }

    /// <summary>
    /// Tries to get a measurement for the specified exchange rate.
    /// </summary>
    /// <param name="exchangeRate">The exchange rate.</param>
    /// <param name="measurement">When this method returns, contains the measurement if found; otherwise, null.</param>
    /// <returns>true if the measurement was found; otherwise, false.</returns>
    public bool TryGetMeasurement(decimal exchangeRate, [NotNullWhen(true)] out IMeasurement? measurement)
    {
        if (TryGetMeasureUnit(GetMeasureUnitCode(), exchangeRate, out Enum? measureUnit) && measureUnit is not null)
        {
            measurement = GetMeasurement(measureUnit);

            return true;
        }

        measurement = null;

        return false;
    }

    /// <summary>
    /// Tries to set a custom name for the measurement.
    /// </summary>
    /// <param name="customName">The custom name.</param>
    /// <returns>true if the custom name was set successfully; otherwise, false.</returns>
    public bool TrySetCustomName(string? customName)
    {
        return TrySetCustomName(GetBaseMeasureUnit(), customName);
    }
    #endregion
}
