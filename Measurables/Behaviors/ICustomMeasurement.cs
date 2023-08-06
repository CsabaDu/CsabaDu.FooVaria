namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface ICustomMeasurement : ICustomMeasureUnit
    {
        ICustomMeasurement GetNextCustomMeasurement(MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
        bool TryGetCustomMeasurement(Enum measureUnit, decimal exchangeRate, [NotNullWhen(true)] out ICustomMeasurement? customMeasurement);

        void InitiateCustomExchangeRates(MeasureUnitTypeCode measurementUnitTypeCode, params decimal[] exchangeRates);
        void ValidateCustomMeasureUnitTypeCode(MeasureUnitTypeCode measurementUnitTypeCode);
    }

    public interface ICustomMeasureUnit
    {
        bool IsCustomMeasureUnit(Enum measureUnit);
        bool TryAddCustomMeasureUnit(Enum measureUnit, decimal exchangeRate, string? customMeasureUnitName = null);
        bool TryAddCustomMeasureUnitName(Enum measureUnit, string customMeasureUnitName);
        IDictionary<Enum, string> GetCustomMeasureUnitNameCollection(MeasureUnitTypeCode? measureUnitTypeCode = null);

        void ValidateExchangeRate(decimal? exchangeRate, Enum? measureUnit = null);
    }
}
