namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IShapeExtentType
{
    bool TryGetShapeExtentCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentCode? shapeExtentCode);
    IEnumerable<ShapeExtentCode> GetShapeExtentCodes();
    bool IsValidShapeExtentCode(ShapeExtentCode shapeExtentCode);

    //void ValidateShapeExtentCode(ShapeExtentCode shapeExtentCode, string paramName);
}
