namespace CsabaDu.FooVaria.BaseTypes.Shapes.Behaviors
{
    public interface IShapeComponents : ISimpleShapeComponentCount
    {
        IEnumerable<ISimpleShapeComponent> GetShapeComponents();
        ISimpleShapeComponent? GetValidSimpleShapeComponent(IQuantifiable? simpleShapeComponent);

    }

    public interface IShapeComponents<out T> : IShapeComponents where T : class/*, IQuantifiable*/, ISimpleShapeComponent // Vissza!
    {
        IEnumerable<T>? GetShapeComponents(IShape shape);
    }
}