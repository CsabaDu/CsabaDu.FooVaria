namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors
{
    public interface IBaseShapeComponents : IMeasureUnitCode
    {
        IEnumerable<IShapeComponent> GetBaseShapeComponents();
    }
}
