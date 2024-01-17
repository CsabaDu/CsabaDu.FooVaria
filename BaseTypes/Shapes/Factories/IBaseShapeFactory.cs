namespace CsabaDu.FooVaria.BaseShapes.Factories;

public interface IBaseShapeFactory : IBaseSpreadFactory
{
    IBaseShape? CreateBaseBaseShape(params IShapeComponent[] baseShapeComponents);
}
