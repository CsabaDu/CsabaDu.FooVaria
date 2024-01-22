namespace CsabaDu.FooVaria.BaseShapes.Behaviors
{
    public interface IShapeComponents
    {
        IEnumerable<IShapeComponent> GetShapeComponents();
    }

    public interface IShapeComponents<out T> : IShapeComponents where T : class/*, IQuantifiable*/, IShapeComponent // Vissza!
    {
        IEnumerable<T>? GetShapeComponents(IBaseShape baseBaseShape);
    }
}