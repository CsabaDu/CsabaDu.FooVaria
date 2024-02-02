namespace CsabaDu.FooVaria.Shapes.Behaviors;

public interface IShapeExtent
{
    IExtent GetShapeExtent(ShapeExtentCode shapeExtentCode);

    void ValidateShapeExtent(IExtent? shapeExtent, string paramName);
}
