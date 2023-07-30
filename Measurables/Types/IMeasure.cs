namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasure : IBaseMeasure, ILimitable, ICalculable, ICalculate<decimal, IMeasure>
{
    IMeasure GetMeasure(ValueType quantity, Enum measureUnit, decimal? exchangeRate = null);
    IMeasure GetMeasure(ValueType quantity, IMeasurement? measurement = null);
    IMeasure GetMeasure(IBaseMeasure baseMeasure);
    IMeasure GetMeasure(IMeasure? other = null);
    IMeasure GetMeasure(Enum measureUnit);

    bool TryGetValidMeasureQuantity(ValueType quantity, Enum measureUnit, out ValueType? measureQuantity);
}
