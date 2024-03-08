namespace CsabaDu.FooVaria.ShapeLimits.Types;

public interface IShapeLimit : ISimpleShape, ILimiter<IShapeLimit, IShape>, ICommonBase<IShapeLimit>
{
    LimitMode LimitMode { get; init; }
    ISimpleShape SimpleShape { get; init; }
    IShapeLimitFactory Factory { get; init; }

    IShapeLimit GetShapeLimit(ISimpleShape simpleShape, LimitMode limitMode);
    IShapeLimit? GetShapeLimit(LimitMode limitMode, params IShapeComponent[] shapeComponents);
    ISimpleShapeFactory GetSimpleShapeFactory();
    void ValidateSimpleShape(ISimpleShape? simpleShape, string paramName);
}
