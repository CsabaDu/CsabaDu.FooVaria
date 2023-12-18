namespace CsabaDu.FooVaria.Shapes.Behaviors;

public interface IDimensions
{
    IEnumerable<IExtent> GetDimensions();
    IEnumerable<IExtent> GetSortedDimensions();
}
