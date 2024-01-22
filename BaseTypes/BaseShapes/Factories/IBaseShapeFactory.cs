namespace CsabaDu.FooVaria.BaseShapes.Factories;

public interface IBaseShapeFactory : IBaseSpreadFactory
{
    IBaseShape? CreateBaseShape(params IShapeComponent[] baseShapeComponents);
}
