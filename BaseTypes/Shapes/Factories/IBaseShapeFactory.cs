namespace CsabaDu.FooVaria.BaseTypes.Shapes.Factories;

public interface IShapeFactory : ISpreadFactory
{
    IShape? CreateShape(params IShapeComponent[] shapeComponents);
}
