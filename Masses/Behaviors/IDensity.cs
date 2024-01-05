namespace CsabaDu.FooVaria.Masses.Behaviors
{
    public interface IDensity
    {
        IProportion<WeightUnit, VolumeUnit> GetDensity();
    }
}
