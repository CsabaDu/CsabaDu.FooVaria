using CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Factories;

namespace CsabaDu.FooVaria.ShapeLimits.Types;

public interface IShapeLimit : ISimpleShape, ILimiter<IShapeLimit, IShape>, ICommonBase<IShapeLimit>
{
    LimitMode LimitMode { get; init; }
    ISimpleShape SimpleShape { get; init; }

    IShapeLimit GetShapeLimit(ISimpleShape simpleShape, LimitMode limitMode);
    ISimpleShapeFactory GetSimpleShapeFactory();
    void ValidateSimpleShape(ISimpleShape? simpleShape, string paramName);
}
