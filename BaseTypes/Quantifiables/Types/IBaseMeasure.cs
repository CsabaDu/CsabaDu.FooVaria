using CsabaDu.FooVaria.Measurables.Enums;
using CsabaDu.FooVaria.Quantifiables.Enums;

namespace CsabaDu.FooVaria.Quantifiables.Types;

public interface IBaseMeasure : IQuantifiable, IExchangeRate, IDecimalQuantity, IQuantityTypeCode, IRateComponentCode, IExchange<IBaseMeasure, Enum>, IRound<IBaseMeasure>
{
    IBaseMeasurement Measurement { get; init; }
    object Quantity { get; init; }
    RateComponentCode RateComponentCode { get; }

    IBaseMeasure GetBaseMeasure(Enum measureUnit, ValueType quantity);
    IBaseMeasure GetBaseMeasure(ValueType quantity);
    IBaseMeasure GetBaseMeasure(string name, ValueType quantity);
    IBaseMeasure GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity);
    IBaseMeasure? GetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName);
    IBaseMeasure? GetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity);

    void ValidateQuantifiable(IQuantifiable? quantifiable, string paramName);

    #region Default implementations
    public bool TryGetBaseMeasure(Enum measureUnit, decimal exchangeRate, ValueType quantity, string customName, [NotNullWhen(true)] out IBaseMeasure? baseMeasure)
    {
        baseMeasure = GetBaseMeasure(measureUnit, exchangeRate, quantity, customName);

        return baseMeasure != null;
    }

    public bool TryGetBaseMeasure(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate, ValueType quantity, [NotNullWhen(true)] out IBaseMeasure? baseMeasure)
    {
        baseMeasure = GetBaseMeasure(customName, measureUnitCode, exchangeRate, quantity);

        return baseMeasure != null;
    }
    #endregion
}