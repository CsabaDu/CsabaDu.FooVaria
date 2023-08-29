namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IVolumetricWeight<out T> where T : class, IMeasure
{
    T GetVolumetricWeight(IVolume volume, decimal ratio, bool isGreater = true);
}
