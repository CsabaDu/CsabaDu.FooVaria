namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface IShape : ISpread, IFit<IShape>, IShapeComponents, IMeasureUnitCodes, IBaseShape, IBaseShapeComponents, IEqualityComparer<IShape>
{
    IShape? GetShape(params IShapeComponent[] shapeComponents);

    void ValidateShapeComponent(IQuantifiable? quantifiable, string paramName);
}
