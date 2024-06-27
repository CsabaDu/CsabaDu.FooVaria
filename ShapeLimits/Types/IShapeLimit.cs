using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.ShapeLimits.Types;

public interface IShapeLimit : IShape, ILimiter<IShapeLimit, IShape>, IDeepCopy<IShapeLimit>, IConcreteFactory<IShapeLimitFactory>
{
    LimitMode LimitMode { get; init; }
    ISimpleShape SimpleShape { get; init; }
    //IShapeLimitFactory Factory { get; init; }

    IShapeLimit GetShapeLimit(ISimpleShape simpleShape, LimitMode limitMode);
    IShapeLimit? GetShapeLimit(LimitMode limitMode, params IShapeComponent[] shapeComponents);
    ISimpleShapeFactory GetSimpleShapeFactory();
    void ValidateSimpleShape(ISimpleShape? simpleShape, string paramName);
}
