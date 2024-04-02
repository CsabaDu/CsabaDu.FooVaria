namespace CsabaDu.FooVaria.Masses.Behaviors;

public interface IDensity
{
    IProportion GetDensity();
    void ValidateDensity(IProportion density, string paramName);
}
