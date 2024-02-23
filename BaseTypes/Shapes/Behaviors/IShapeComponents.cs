namespace CsabaDu.FooVaria.BaseTypes.Shapes.Behaviors
{
    public interface IShapeComponents : IShapeComponentCount
    {
        IEnumerable<IShapeComponent> GetShapeComponents();
        IShapeComponent? GetValidShapeComponent(IBaseQuantifiable? shapeComponent);

    }

    public interface IShapeComponents<out T> : IShapeComponents where T : class, IBaseQuantifiable, IShapeComponent
    {
        IEnumerable<T> GetShapeComponents(IShape shape);
    }
}