namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class CustomMeasurement(IMeasurementFactory factory, Enum measureUnit) : Measurement(factory, measureUnit), ICustomMeasurement
{
    #region Public methods
    public IEnumerable<Enum> GetNotUsedCustomMeasureUnits()
    {
        return GetNotUsedCustomMeasureUnits(GetMeasureUnitCode());

        //IEnumerable<MeasureUnitCode> measureUnitCodes = GetMeasureUnitCodes();
        //IEnumerable<Enum> notUsedCustomMeasureUnits = GetNotUsedCustomMeasureUnits(measureUnitCodes.First());

        //for (int i = 1; i < measureUnitCodes.Count(); i++)
        //{
        //    MeasureUnitCode measureUnitCode = measureUnitCodes.ElementAt(i);
        //    IEnumerable<Enum> next = GetNotUsedCustomMeasureUnits(measureUnitCode);
        //    notUsedCustomMeasureUnits = notUsedCustomMeasureUnits.Union(next);
        //}

        //return notUsedCustomMeasureUnits;
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

    public bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, string customName, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement)
    {
        customMeasurement = null;

        if (TrySetCustomMeasureUnit(measureUnit, exchangeRate, customName))
        {
            customMeasurement = (ICustomMeasurement)GetMeasurement(measureUnit);
        }

        return customMeasurement != null;
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
    private static void SetExchangeRate(Enum measureUnit, decimal exchangeRate)
    {
        if (TrySetExchangeRate(DefinedMeasureUnit(measureUnit, nameof(measureUnit)), exchangeRate)) return;

        throw DecimalArgumentOutOfRangeException(nameof(exchangeRate), exchangeRate);
    }
    #endregion
    #endregion
}