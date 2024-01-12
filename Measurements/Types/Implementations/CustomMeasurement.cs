namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class CustomMeasurement : Measurement, ICustomMeasurement
{
    #region Constructors
    internal CustomMeasurement(IMeasurementFactory factory, Enum measureUnit) : base(factory, measureUnit)
    {
    }
    #endregion

    #region Public methods
    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits(MeasureUnitCode measureUnitCode)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        Type measureUnitType = MeasureUnitTypes.GetMeasureUnitType(measureUnitCode);
        IEnumerable<Enum> customMeasureUnits = Enum.GetValues(measureUnitType).Cast<Enum>();

        return customMeasureUnits.Where(x => !ExchangeRateCollection.ContainsKey(x));
    }

    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
    {
        IEnumerable<MeasureUnitCode> measureUnitCodes = GetMeasureUnitCodes();
        IEnumerable<Enum> notUsedCustomMeasureUnits = GetNotUsedCustomMeasureUnits(measureUnitCodes.First());

        for (int i = 1; i < measureUnitCodes.Count(); i++)
        {
            MeasureUnitCode measureUnitCode = measureUnitCodes.ElementAt(i);
            IEnumerable<Enum> next = GetNotUsedCustomMeasureUnits(measureUnitCode);
            notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
        }

        return notUsedCustomMeasureUnits;
    }

    public void InitializeCustomExchangeRates(MeasureUnitCode measureUnitCode, IDictionary<string, decimal> customExchangeRateCollection)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        int count = NullChecked(customExchangeRateCollection, nameof(customExchangeRateCollection)).Count;

        if (count == 0) throw CountArgumentOutOfRangeException(count, nameof(customExchangeRateCollection));

        for (int i = 0; i < count; i++)
        {
            string? customName = customExchangeRateCollection.Keys.ElementAt(i);
            decimal exchangeRate = customExchangeRateCollection!.Values.ElementAt(i);
            Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).ElementAt(i);

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

    public void SetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        ValidateCustomMeasureUnitCode(measureUnitCode);

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).OrderBy(x => x).First();

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

        return trySetCustomMeasureUnit(this, measureUnit, exchangeRate, customName);

        #region Local methods
        static bool trySetCustomMeasureUnit(IMeasurement measurement, Enum measureUnit, decimal exchangeRate, string customName)
        {
            if (!TrySetExchangeRate(measureUnit, exchangeRate)) return false;

            if (measurement.TrySetCustomName(measureUnit, customName)) return true;

            if (RemoveExchangeRate(measureUnit)) return false;

            throw new InvalidOperationException(null);
        }
        #endregion
    }

    public bool TrySetCustomMeasureUnit(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate)
    {
        if (!measureUnitCode.IsCustomMeasureUnitCode()) return false;

        Enum measureUnit = GetNotUsedCustomMeasureUnits(measureUnitCode).OrderBy(x => x).First();

        return TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName);
    }

    public void ValidateCustomMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        if (measureUnitCode.IsCustomMeasureUnitCode()) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode);
    }

    #region Override methods
    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes().Where(x => x.IsCustomMeasureUnitCode());
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