namespace CsabaDu.FooVaria.Measures.Types;

public interface ITimePeriod : IMeasure<ITimePeriod, double, TimePeriodUnit>, IConvertMeasure<ITimePeriod, TimeSpan>
{
}
