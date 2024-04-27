namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface IShapeComponent : IMeasureUnitCode, IEqualityComparer<IShapeComponent>
{
    IEnumerable<IShapeComponent> GetBaseShapeComponents();
}
