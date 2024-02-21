namespace CsabaDu.FooVaria.ShapeLimits.Factories;

public interface IShapeLimitFactory : ISimpleShapeFactory, IFactory<IShapeLimit>
{
    IShapeLimit Create(ISimpleShape simpleShape, LimitMode limitMode);
}
