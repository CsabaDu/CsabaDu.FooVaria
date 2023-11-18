namespace CsabaDu.FooVaria.RateComponents.Types.MeasureTypes;

public interface ITimePeriod : IMeasure<ITimePeriod, double, TimePeriodUnit>, IConvertMeasure<ITimePeriod, TimeSpan>
{
}
