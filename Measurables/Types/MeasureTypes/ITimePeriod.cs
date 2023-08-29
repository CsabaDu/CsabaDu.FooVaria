namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface ITimePeriod : IMeasure, IConvertMeasure<ITimePeriod, TimeSpan>
{
    ITimePeriod GetTimePeriod(double quantity, TimePeriodUnit timePeriodUnit);
    ITimePeriod GetTimePeriod(ValueType quantity, string name);
    ITimePeriod GetTimePeriod(ValueType quantity, IMeasurement measurement);
    ITimePeriod GetTimePeriod(IBaseMeasure baseMeasure);
    ITimePeriod GetTimePeriod(ITimePeriod? other = null);
}
