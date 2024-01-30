namespace CsabaDu.FooVaria.BaseTypes.BaseShapes.Types;

public interface IBaseShape : IBaseSpread, IFit<IBaseShape>, IShapeComponents/*, IShapeComponentCount*/
{
    IBaseShape? GetBaseShape(params IShapeComponent[] shapeComponents);
    //IQuantifiable? GetValidShapeComponent(IShapeComponent shapeComponent);

    void ValidateShapeComponent(IQuantifiable shapeComponent, string paramName);
}


// Visszaállítani