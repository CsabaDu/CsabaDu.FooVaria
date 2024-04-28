namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
{
    public interface IBaseShapeComponents : IMeasureUnitCode, IEqualityComparer<IShapeComponent>
    {
        IEnumerable<IShapeComponent> GetBaseShapeComponents();
    }
}
