namespace CsabaDu.FooVaria.ShapeLimits.Factories;

public interface IShapeLimitFactory : IShapeFactory, IFactory<IShapeLimit>
{
    ISimpleShapeFactory SimpleShapeFactory { get; init; }
    IShapeLimit Create(ISimpleShape simpleShape, LimitMode limitMode);
    IShapeLimit? Create(LimitMode limitMode, params IShapeComponent[] shapeComponents);
}
