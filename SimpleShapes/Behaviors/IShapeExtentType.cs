namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IShapeExtentType
{
    bool TryGetShapeExtentCode(IExtent simpleShapeExtent, [NotNullWhen(true)] out ShapeExtentCode? simpleShapeExtentCode);
    IEnumerable<ShapeExtentCode> GetShapeExtentCodes();
    bool IsValidShapeExtentCode(ShapeExtentCode simpleShapeExtentCode);

    //void ValidateShapeExtentCode(ShapeExtentCode simpleShapeExtentCode, string paramName);
}
