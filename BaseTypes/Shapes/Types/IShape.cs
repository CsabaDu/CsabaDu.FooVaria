namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface IShape : IBaseSpread, IFit<IShape>, IShapeComponents/*, IShapeComponentCount*/
{
    IShape? GetShape(params IShapeComponent[] shapeComponents);

    void ValidateShapeComponent(IQuantifiable simpleShapeComponent, string paramName);
}


// Visszaállítani