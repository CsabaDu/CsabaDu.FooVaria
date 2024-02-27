namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Behaviors;

public interface IShapeExtent
{
    IExtent GetShapeExtent(ShapeExtentCode shapeExtentCode);

    void ValidateShapeExtent(IExtent? shapeExtent, [DisallowNull] string paramName);
}
