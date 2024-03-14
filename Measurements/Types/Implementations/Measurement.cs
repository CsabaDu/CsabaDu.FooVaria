﻿namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

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
    public static decimal GetExchangeRate(string name)
    {
        const string paramName = nameof(name);
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, paramName));

        if (measureUnit != null) return GetExchangeRate(measureUnit, paramName);

        throw NameArgumentOutOfRangeException(name);
    }

    public override sealed IMeasurementFactory GetFactory()
    {
        return Factory;
    }

    public override sealed Enum GetBaseMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    public override sealed MeasureUnitCode GetMeasureUnitCode()
    {
        Enum measuureUnit = GetBaseMeasureUnit();

        return GetMeasureUnitCode(measuureUnit);
    }

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

    public static Dictionary<object, string> GetCustomNameCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);
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
        if (TryGetMeasureUnit(GetMeasureUnitCode(), exchangeRate, out Enum? measureUnit) && measureUnit != null)
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

    public override sealed void ValidateExchangeRate(decimal exchangeRate, string paramName)
    {
        if (exchangeRate == GetExchangeRate()) return;

        throw DecimalArgumentOutOfRangeException(paramName, exchangeRate);
    }

    public static void ValidateCustomName(string? customName)
    {
        if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

        throw NameArgumentOutOfRangeException(nameof(customName), customName!);
    }
    #endregion
}