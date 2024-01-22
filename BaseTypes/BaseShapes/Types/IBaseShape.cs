namespace CsabaDu.FooVaria.BaseShapes.Types;
public interface IBaseShape : IBaseSpread, IShapeComponentCount, IFit<IBaseShape>, IShapeComponents
{
    IBaseShape? GetBaseShape(params IShapeComponent[] shapeComponents);
    IQuantifiable? GetValidBaseShapeComponent(IShapeComponent shapeComponent);

    void ValidateBaseShapeComponent(IQuantifiable shapeComponent, string paramName);
}


// Visszaállítani