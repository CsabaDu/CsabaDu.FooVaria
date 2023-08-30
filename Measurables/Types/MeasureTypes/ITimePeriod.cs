namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface ITimePeriod : IMeasure, IMeasure<ITimePeriod, double, TimePeriodUnit>, IConvertMeasure<ITimePeriod, TimeSpan>
{
    ITimePeriod GetTimePeriod(IBaseMeasure baseMeasure);
}

    //ITimePeriod GetTimePeriod(double quantity, TimePeriodUnit timePeriodUnit);
    //ITimePeriod GetTimePeriod(ValueType quantity, string name);
    //ITimePeriod GetTimePeriod(ValueType quantity, IMeasurement? measurement = null);
    //ITimePeriod GetTimePeriod(ITimePeriod? other = null);