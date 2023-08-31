namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface ITimePeriod : IMeasure, IMeasure<ITimePeriod, double, TimePeriodUnit>, IConvertMeasure<ITimePeriod, TimeSpan>
{
}
