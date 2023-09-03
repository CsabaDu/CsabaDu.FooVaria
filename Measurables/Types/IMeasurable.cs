namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurable : IBaseMeasurable, IQuantityType
{
    IMeasurableFactory MeasurableFactory { get; init; }

    IMeasurable GetDefault();

    IMeasurable GetMeasurable(IMeasurable? other = null);
    IMeasurable GetMeasurable(IMeasurableFactory measurableFactory, IMeasurable measurable);
}
