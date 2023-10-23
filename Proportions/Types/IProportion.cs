namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate
    {
        IProportion GetProportion(IBaseRate baseRate);
        IProportion GetProportion(Enum numeratorContext, ValueType quantity, Enum denominatorContext);
        IProportion GetProportion(IMeasurement numeratorMeasurement, ValueType quantity, IMeasurement denominatorMeasurement);
    }

    public interface IDensity : IProportion
    {
        
    }
}
