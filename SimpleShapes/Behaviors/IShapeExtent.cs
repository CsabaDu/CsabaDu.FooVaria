namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IShapeExtent
{
    IExtent GetShapeExtent(ShapeExtentCode simpleShapeExtentCode);

    void ValidateShapeExtent(IExtent? simpleShapeExtent, string paramName);
}
