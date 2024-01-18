﻿using CsabaDu.FooVaria.BaseMeasurements.Statics;

namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal abstract class Measurement : BaseMeasurement, IMeasurement
{
    #region Constructors
    private protected Measurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
        ExchangeRate = GetExchangeRate(measureUnit);
        MeasureUnit = measureUnit;
    }
    #endregion

    #region Properties
    public object MeasureUnit { get; init; }
    public decimal ExchangeRate { get; init; }

    #region Static prpperties
    public static IDictionary<object, string> CustomNameCollection { get; protected set; } = new Dictionary<object, string>();
    #endregion
    #endregion

    #region Public methods
    public string? GetCustomName(Enum measureUnit)
    {
        return CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value;
    }

    public string? GetCustomName()
    {
        return GetCustomName(GetMeasureUnit());
    }

    public IDictionary<object, string> GetCustomNameCollection(MeasureUnitCode measureUnitCode)
    {
        return GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);
    }

    public IMeasurement GetDefault()
    {
        return GetDefault(MeasureUnitCode)!;
    }

    public IMeasurement? GetDefault(MeasureUnitCode measureUnitCode)
    {
        return GetFactory().CreateDefault(measureUnitCode);
    }

    public string GetDefaultName()
    {
        return GetDefaultName(GetMeasureUnit());
    }

    public string GetDefaultName(Enum measureUnit)
    {
        return MeasureUnitTypes.GetDefaultName(GetMeasureUnit());
    }

    public IMeasurement GetMeasurement(Enum measureUnit)
    {
        return GetFactory().Create(measureUnit);
    }

    public IMeasurement GetMeasurement(IMeasurement other)
    {
        return GetFactory().CreateNew(other);
    }

    public IMeasurement GetMeasurement(string name)
    {
        return GetFactory().Create(name);
    }

    public IMeasurement? GetMeasurement(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        return GetFactory().Create(customName, measureUnitCode, exchangeRate);
    }

    public IMeasurement? GetMeasurement(Enum measureUnit, decimal exchangeRate, string customName)
    {
        return GetFactory().Create(measureUnit, exchangeRate, customName);
    }

    public Enum? GetMeasureUnit(string name)
    {
        name ??= string.Empty;

        return (Enum)GetMeasureUnitCollection().FirstOrDefault(x => x.Key == name).Value;
    }

    public IDictionary<string, object> GetMeasureUnitCollection(MeasureUnitCode measureUnitCode)
    {
        IEnumerable<object> validMeasureUnits = GetValidMeasureUnits(measureUnitCode);
        IDictionary<object, string> customNameCollection = GetMeasureUnitBasedCollection(CustomNameCollection, measureUnitCode);

        return GetMeasureUnitCollection(validMeasureUnits, customNameCollection);
    }

    public IDictionary<string, object> GetMeasureUnitCollection()
    {
        IEnumerable<object> validMeasureUnits = MeasureUnits.GetValidMeasureUnits();

        return GetMeasureUnitCollection(validMeasureUnits, CustomNameCollection);
    }

    public IEnumerable<object> GetValidMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        return MeasureUnits.GetValidMeasureUnits().Where(x => x.GetType().Equals(measureUnitCode.GetMeasureUnitType()));
    }

    public void RestoreCustomNameCollection()
    {
        CustomNameCollection.Clear();
    }

    public void SetCustomName(Enum measureUnit, string customName)
    {
        if (TrySetCustomName(measureUnit, customName)) return;

        throw NameArgumentOutOfRangeException(customName);
    }

    public void SetOrReplaceCustomName(string customName)
    {
        ValidateCustomName(customName);

        Enum measureUnit = GetMeasureUnit();

        if (CustomNameCollection.TryAdd(measureUnit, customName)) return;

        if (CustomNameCollection.Remove(measureUnit)
            && CustomNameCollection.TryAdd(measureUnit, customName))
            return;

        throw new InvalidOperationException(null);
    }

    public bool TryGetMeasurement(decimal exchangeRate, [NotNullWhen(true)] out IMeasurement? measurement)
    {
        if (TryGetMeasureUnit(MeasureUnitCode, exchangeRate, out Enum? measureUnit) && measureUnit != null)
        {
            measurement = GetMeasurement(measureUnit);

            return true;
        }

        measurement = null;

        return false;
    }

    public bool TryGetMeasureUnit(MeasureUnitCode measureUnitCode, decimal exchangeRate, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = (Enum)GetExchangeRateCollection(measureUnitCode).FirstOrDefault(x => x.Value == exchangeRate).Key;

        return measureUnit != null;
    }

    public bool TryGetMeasureUnit(string name, [NotNullWhen(true)] out Enum? measureUnit)
    {
        measureUnit = GetMeasureUnit(name);

        return measureUnit != null;
    }

    public bool TrySetCustomName(Enum? measureUnit, string? customName)
    {
        if (measureUnit == null) return false;

        if (customName == null) return false;

        if (!IsValidMeasureUnit(measureUnit)) return false;

        if (CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value == customName) return true;

        if (!IsValidCustomNameParam(customName)) return false;

        return CustomNameCollection.TryAdd(measureUnit, customName);
    }

    public void ValidateCustomName(string? customName)
    {
        if (IsValidCustomNameParam(NullChecked(customName, nameof(customName)))) return;

        throw NameArgumentOutOfRangeException(nameof(customName), customName!);
    }

    #region Override methods
    #region Sealed methods
    public override sealed decimal GetExchangeRate()
    {
        return ExchangeRate;
    }

    public override sealed decimal GetExchangeRate(string name)
    {
        Enum? measureUnit = GetMeasureUnit(NullChecked(name, nameof(name)));

        if (measureUnit != null) return GetExchangeRate(measureUnit);

        throw NameArgumentOutOfRangeException(name);
    }

    public override sealed IMeasurementFactory GetFactory()
    {
        return (IMeasurementFactory)Factory;
    }

    public override sealed Enum GetMeasureUnit()
    {
        return (Enum)MeasureUnit;
    }

    public override sealed string GetName()
    {
        return GetCustomName() ?? GetDefaultName();
    }

    public override sealed void RestoreConstantExchangeRates()
    {
        RestoreCustomNameCollection();
        ExchangeRateCollection = new Dictionary<object, decimal>(ConstantExchangeRateCollection);
    }
    #endregion
    #endregion
    #endregion

    #region Protected methods
    #region Static methods
    protected static bool IsValidCustomNameParam(string? customName)
    {
        return !string.IsNullOrWhiteSpace(customName)
            && customName != string.Empty
            && doesNotContainCustomName(CustomNameCollection.Values)
            && doesNotContainCustomName(GetDefaultNames());

        #region Local methods
        bool doesNotContainCustomName(IEnumerable<string> names)
        {
            return !names.Any(x => x.ToLower() == customName.ToLower());
        }
        #endregion
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static IDictionary<string, object> GetMeasureUnitCollection(IEnumerable<object> validMeasureUnits, IDictionary<object, string> customNameCollection)
    {
        IDictionary<string, object> measureUnitCollection = validMeasureUnits.ToDictionary
            (
                x => MeasureUnitTypes.GetDefaultName((Enum)x),
                x => x
            );

        foreach (KeyValuePair<object, string> item in customNameCollection)
        {
            measureUnitCollection.Add(item.Value, item.Key);
        }

        return measureUnitCollection;
    }
    #endregion
    #endregion
}