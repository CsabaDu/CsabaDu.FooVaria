namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface IShape : ISpread, IFit<IShape>, IShapeComponents, ILimitable
{
    IShape GetShape();
    IShape? GetShape(params IShapeComponent[] shapeComponents);

    void ValidateShapeComponent(IBaseQuantifiable? shapeComponent, string paramName);
}
