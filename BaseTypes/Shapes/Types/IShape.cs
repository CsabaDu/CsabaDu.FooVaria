namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface IShape : ISpread, IFit<IShape>, IShapeComponents
{
    IShape? GetShape(params IShapeComponent[] shapeComponents);

    void ValidateShapeComponent(IBaseQuantifiable? shapeComponent, string paramName);
}
