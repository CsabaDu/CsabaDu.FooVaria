namespace CsabaDu.FooVaria.BaseTypes.BaseShapes.Factories;

public interface IBaseShapeFactory : IBaseSpreadFactory
{
    IBaseShape? CreateBaseShape(params IShapeComponent[] shapeComponents);
}
