namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface ICash : IMeasure, IMeasure<ICash, decimal, Currency>, ICustomMeasure<ICash, decimal, Currency>
{
}
