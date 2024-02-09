namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types;

public interface IShape : IBaseSpread, IFit<IShape>, IShapeComponents/*, ISimpleShapeComponentCount*/
{
    IShape? GetShape(params ISimpleShapeComponent[] shapeComponents);

    void ValidateSimpleShapeComponent(IQuantifiable simpleShapeComponent, string paramName);
}


// Visszaállítani