namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasurableFactory
{
    IMeasurable Create(IMeasurable other);
    IMeasurable CreateDefault(IMeasurable measurable);
}
