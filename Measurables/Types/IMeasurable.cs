namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurable : IBaseMeasurable, IQuantityType
{
    IMeasurableFactory MeasurableFactory { get; init; }

    IMeasurable GetDefault();

    IMeasurable GetMeasurable(IMeasurable other);
    IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable);
}
