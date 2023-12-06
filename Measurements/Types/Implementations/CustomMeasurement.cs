namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class CustomMeasurement : Measurement, ICustomMeasurement
{
    #region Constructors
    internal CustomMeasurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }
    #endregion

    #region Public methods
    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitTypeCode measureUnitTypeCode)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitTypeCode);
        Array customMeasureUnits = Enum.GetValues(measureUnitType);

        foreach (Enum item in customMeasureUnits)
        {
            if (!ExchangeRateCollection.ContainsKey(item))
            {
                yield return item;
            }
        }
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
    {
        IEnumerable<MeasureUnitTypeCode> measureUnitTypeCodes = GetMeasureUnitTypeCodes();
        IEnumerable<Enum> notUsedCustomMeasureUnits = GetNotUsedCustomMeasureUnits(measureUnitTypeCodes.First());

        for (int i = 1; i < measureUnitTypeCodes.Count(); i++)
        {
            MeasureUnitTypeCode measureUnitTypeCode = measureUnitTypeCodes.ElementAt(i);
            IEnumerable<Enum> next = GetNotUsedCustomMeasureUnits(measureUnitTypeCode);
            notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
        }

        return notUsedCustomMeasureUnits;
    }

    public void InitializeCustomExchangeRates(MeasureUnitTypeCode measureUnitTypeCode, IDictionary<string, decimal> customExchangeRateCollection)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        int count = NullChecked(customExchangeRateCollection, nameof(customExchangeRateCollection)).Count;

        if (count == 0) throw new ArgumentOutOfRangeException(nameof(customExchangeRateCollection), count, null);

        for (int i = 0; i < count; i++)
        {
            string? customName = customExchangeRateCollection.Keys.ElementAt(i);
            decimal exchangeRate = customExchangeRateCollection!.Values.ElementAt(i);
            Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).ElementAt(i);

            if (customName == null
                || customName == CustomNameCollection.FirstOrDefault(x => x.Key == measureUnit).Value
                || IsValidCustomNameParam(customName)
                && CustomNameCollection.TryAdd(measureUnit, customName))
            {
                SetExchangeRate(measureUnit, exchangeRate);
            }
        }
    }

    public void SetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    public void SetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        ValidateCustomMeasureUnitTypeCode(measureUnitTypeCode);

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

        SetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public void SetOrReplaceCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsCustomMeasureUnit(NullChecked(measureUnit, nameof(measureUnit)))) throw InvalidMeasureUnitEnumArgumentException(measureUnit);

        if (!exchangeRate.IsValidExchangeRate()) throw DecimalArgumentOutOfRangeException(exchangeRate);

        if (!IsValidCustomNameParam(customName)) throw NameArgumentOutOfRangeException(nameof(customName), customName);

        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

        if (!CustomNameCollection.Remove(measureUnit, out string? removedCustomName)) throw new InvalidOperationException(null);

        if (!RemoveExchangeRate(measureUnit))
        {
            SetCustomName(measureUnit, removedCustomName);
        }

        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName)) return;

        SetExchangeRate(measureUnit, GetExchangeRate(measureUnit));
    }

    public bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement)
    {
        customMeasurement = null;

        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName))
        {
            customMeasurement = (ICustomMeasurement)GetMeasurement(measureUnit);
        }

        return customMeasurement != null;
    }

    public bool TrySetCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!IsCustomMeasureUnit(measureUnit)) return false;

        if (!exchangeRate.IsValidExchangeRate()) return false;

        return TrySetCustomMeasureUnit(this, measureUnit, exchangeRate, customName);
    }

    public bool TrySetCustomMeasureUnit(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        if (!measureUnitTypeCode.IsCustomMeasureUnitTypeCode()) return false;

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitTypeCode).OrderBy(x => x).First();

        return TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measureUnitTypeCode)
    {
        if (measureUnitTypeCode.IsCustomMeasureUnitTypeCode()) return;

        throw InvalidMeasureUnitTypeCodeEnumArgumentException(measureUnitTypeCode);
    }

    #region Override methods
    public override IEnumerable<MeasureUnitTypeCode> GetMeasureUnitTypeCodes()
    {
        return base.GetMeasureUnitTypeCodes().Where(x => x.IsCustomMeasureUnitTypeCode());
    }
    #endregion
    #endregion

    #region Private methods
    #region Static methods
    private static bool RemoveExchangeRate(Enum measureUnit)
    {
        return ExchangeRateCollection.Remove(measureUnit);
    }

    private static void SetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        exchangeRate.ValidateExchangeRate();

        if (TrySetExchangeRate(measureUnit, exchangeRate)) return;

        throw InvalidMeasureUnitEnumArgumentException(measureUnit);
    }

    private static bool TrySetCustomMeasureUnit(IMeasurement measurement, Enum measureUnit, decimal exchangeRate, string customName)
    {
        if (!TrySetExchangeRate(measureUnit, exchangeRate)) return false;

        if (measurement.TrySetCustomName(measureUnit, customName)) return true;

        if (RemoveExchangeRate(measureUnit)) return false;

        throw new InvalidOperationException(null);
    }

    private static bool TrySetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (!IsDefinedMeasureUnit(measureUnit)) return false;

        if (ExchangeRateCollection.ContainsKey(measureUnit) && exchangeRate == ExchangeRateCollection[measureUnit]) return true;

        if (exchangeRate <= 0) return false;

        return ExchangeRateCollection.TryAdd(measureUnit, exchangeRate);
    }
    #endregion
    #endregion
}