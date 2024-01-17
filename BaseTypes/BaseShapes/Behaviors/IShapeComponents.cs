namespace CsabaDu.FooVaria.BaseShapes.Behaviors
{
    public interface IShapeComponents
    {
        IEnumerable<IShapeComponent> GetBaseShapeComponents();
    }

    public interface IShapeComponents<out T> : IShapeComponents where T : class/*, IQuantifiable*/, IShapeComponent // Vissza!
    {
        IEnumerable<T>? GetBaseShapeComponents(IBaseShape baseBaseShape);
    }
}