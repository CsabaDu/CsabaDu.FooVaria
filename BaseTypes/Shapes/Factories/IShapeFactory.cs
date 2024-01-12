namespace CsabaDu.FooVaria.Shapes.Factories;

public interface IShapeFactory : IBaseSpreadFactory
{
    IShape? CreateBaseShape(params IShapeComponent[] shapeComponents);
}
