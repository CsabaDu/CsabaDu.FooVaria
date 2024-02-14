namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Behaviors;

public interface IShapeExtent
{
    IExtent GetShapeExtent(ShapeExtentCode simpleShapeExtentCode);

    void ValidateShapeExtent(IExtent? simpleShapeExtent, string paramName);
}
