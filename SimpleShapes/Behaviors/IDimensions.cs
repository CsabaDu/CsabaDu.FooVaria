namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IDimensions
{
    IEnumerable<IExtent> GetDimensions();
    IEnumerable<IExtent> GetSortedDimensions();
}
