namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IVolumetricWeight<out T> where T : class, IMeasure
{
    T GetVolumetricWeight(IVolume volume, decimal ratio, WeightUnit weightUnit = default, bool isGreater = true);
}

