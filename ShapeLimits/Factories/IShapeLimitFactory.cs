using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.ShapeLimits.Factories;

public interface IShapeLimitFactory : IShapeFactory, IDeepCopyFactory<IShapeLimit>, IConcreteFactory
{
    ISimpleShapeFactory SimpleShapeFactory { get; init; }
    IShapeLimit Create(ISimpleShape simpleShape, LimitMode limitMode);
    IShapeLimit? Create(LimitMode limitMode, params IShapeComponent[] shapeComponents);
}
