namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface IShape : ISpread, IFit<IShape>, IShapeComponents, IMeasureUnitCodes, IBaseShapeComponent, IEqualityComparer<IShape>
{
    IShape? GetShape(params IShapeComponent[] shapeComponents);
    IShape GetBaseShape();

    void ValidateShapeComponent(IQuantifiable? quantifiable, string paramName);
}
