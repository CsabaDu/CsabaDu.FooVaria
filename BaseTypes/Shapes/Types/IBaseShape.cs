namespace CsabaDu.FooVaria.BaseShapes.Types;
public interface IBaseShape : IBaseSpread, IShapeComponentCount, IFit<IBaseShape>, IShapeComponents
{
    IBaseShape? GetBaseBaseShape(params IShapeComponent[] baseShapeComponents);
    IQuantifiable? GetValidBaseShapeComponent(IShapeComponent baseShapeComponent);

    void ValidateBaseShapeComponent(IQuantifiable baseShapeComponent, string paramName);
}


// Visszaállítani