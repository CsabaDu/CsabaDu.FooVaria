namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal abstract class Measurement : BaseMeasurement, IMeasurement
{
    #region Constructors
    private protected Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, nameof(factory))
    {
        ValidateMeasureUnit(measureUnit, nameof(measureUnit));

        MeasureUnit = measureUnit;
        Factory = factory;
    }
    #endregion

    #region Properties
    public IMeasurementFactory Factory { get; init; }
    public object MeasureUnit { get; init; }
    #endregion

    #region Public methods

    #region Override methods
    #region Sealed methods
    public override sealed Enum GetBaseMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    public override sealed IMeasurementFactory GetFactory()
    {
        return Factory;
    }

    //public override sealed RateComponentCode GetMeasureUnitCode()
    //{
    //    Enum measuureUnit = GetBaseMeasureUnitReturnValue();

    //    return GetMeasureUnitCode(measuureUnit);
    //}

    public override sealed string GetName()
    {
        return GetCustomName() ?? GetDefaultName();
    }
    #endregion
    #endregion

    public string? GetCustomName()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetCustomName(measureUnit);
    }

    public IDictionary<object, string> GetCustomNameCollection()
    {
        return GetCustomNameCollection(GetMeasureUnitCode());
    }

    public IMeasurement GetDefault()
    {
        return (IMeasurement)GetDefault(GetMeasureUnitCode())!;
    }

    public IMeasurable? GetDefault(MeasureUnitCode measureUnitCode)
    {
        return Factory.CreateDefault(measureUnitCode);
    }

    public string GetDefaultName()
    {
        Enum measureUnit = GetBaseMeasureUnit();

        return GetDefaultName(measureUnit);
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return Factory.Create(measureUnit);
    }

    public IMeasurement GetMeasurement(IMeasurement other)
    {
        return Factory.CreateNew(other);
    }

    public IMeasurement GetMeasurement(string name)
    {
        return Factory.Create(name);
    }

    public IMeasurement? GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        return Factory.Create(customName, measureUnitCode, exchangeRate);
    }

    public IMeasurement? GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return Factory.Create(measureUnit, exchangeRate, customName);
    }

    public void SetCustomName(string customName)
    {
        SetCustomName(GetBaseMeasureUnit(), customName);
    }

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

    public bool TrySetCustomName(string? customName)
    {
        return TrySetCustomName(GetBaseMeasureUnit(), customName);
    }
    #endregion
}