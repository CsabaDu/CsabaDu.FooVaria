namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public abstract class BaseMeasurementFactory : IBaseMeasurementFactory
{
    public abstract IMeasurable Create(IMeasurable other);
}

