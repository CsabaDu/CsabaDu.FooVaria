namespace CsabaDu.FooVaria.BaseTypes.Shapes.Behaviors
{
    public interface IShapeComponents : IShapeComponentCount
    {
        IEnumerable<IShapeComponent> GetShapeComponents();
        IShapeComponent? GetValidShapeComponent(IQuantifiable? simpleShapeComponent);

    }

    public interface IShapeComponents<out T> : IShapeComponents where T : class/*, IQuantifiable*/, IShapeComponent // Vissza!
    {
        IEnumerable<T>? GetShapeComponents(IShape shape);
    }
}