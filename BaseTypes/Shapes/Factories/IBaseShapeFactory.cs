namespace CsabaDu.FooVaria.BaseTypes.Shapes.Factories;

public interface IShapeFactory : IBaseSpreadFactory
{
    IShape? CreateShape(params ISimpleShapeComponent[] shapeComponents);
}
