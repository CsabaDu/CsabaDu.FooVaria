namespace CsabaDu.FooVaria.BaseTypes.BaseShapes.Behaviors
{
    public interface IShapeComponents
    {
        IEnumerable<IShapeComponent> GetShapeComponents();
        IShapeComponent? GetValidShapeComponent(IQuantifiable? shapeComponent);

    }

    public interface IShapeComponents<out T> : IShapeComponents where T : class/*, IQuantifiable*/, IShapeComponent // Vissza!
    {
        IEnumerable<T>? GetShapeComponents(IBaseShape baseBaseShape);
    }
}