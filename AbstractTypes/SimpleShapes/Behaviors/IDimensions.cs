namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Behaviors;

public interface IDimensions
{
    IEnumerable<IExtent> GetDimensions();
    IEnumerable<IExtent> GetSortedDimensions();
}
