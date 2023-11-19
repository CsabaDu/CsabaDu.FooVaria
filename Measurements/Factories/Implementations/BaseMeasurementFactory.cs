namespace CsabaDu.FooVaria.Measurements.Factories.Implementations;

public abstract class BaseMeasurementFactory : IBaseMeasurementFactory
{
    //public abstract IMeasurable Create(IMeasurable other);
    public abstract IMeasurable CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
}

