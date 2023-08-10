namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurable : IBaseMeasurable, IQuantityType
{
    IMeasurableFactory MeasurableFactory { get; init; }

    IMeasurable GetDefaultMeasurable(IMeasurable? measurable = null);
    IMeasurable GetMeasurable(IMeasurable measurable);
    IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable);
}
