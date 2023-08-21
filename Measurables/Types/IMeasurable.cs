namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurable : IBaseMeasurable, IQuantityType
{
    IMeasurableFactory MeasurableFactory { get; init; }

    IMeasurable GetDefault();

    IMeasurable GetMeasurable(IMeasurable? measurable = null);
    IMeasurable GetMeasurable(Enum measureUnit);
    IMeasurable GetMeasurable(string name);
    IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable);
}
