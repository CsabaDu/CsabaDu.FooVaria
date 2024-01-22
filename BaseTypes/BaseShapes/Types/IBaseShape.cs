namespace CsabaDu.FooVaria.BaseShapes.Types;
public interface IBaseShape : IBaseSpread, IShapeComponentCount, IFit<IBaseShape>, IShapeComponents
{
    IBaseShape? GetBaseBaseShape(params IShapeComponent[] shapeComponents);
    IQuantifiable? GetValidBaseShapeComponent(IShapeComponent shapeComponent);

    void ValidateBaseShapeComponent(IQuantifiable shapeComponent, string paramName);
}


// Visszaállítani