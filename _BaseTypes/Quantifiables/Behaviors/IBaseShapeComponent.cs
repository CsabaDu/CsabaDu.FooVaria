namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
{
    public interface IBaseShapeComponent : IMeasureUnitCode
    {
        IEnumerable<IShapeComponent> GetBaseShapeComponents();
    }
}
