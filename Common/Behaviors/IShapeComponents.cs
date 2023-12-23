namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IShapeComponents
    {
        IEnumerable<IShapeComponent> GetShapeComponents();
    }

    public interface IShapeComponents<T> : IShapeComponents where T : class, IQuantifiable, IShapeComponent
    {
        IEnumerable<T> GetShapeComponents(IBaseShape baseShape);
    }
}