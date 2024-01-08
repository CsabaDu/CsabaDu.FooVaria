namespace CsabaDu.FooVaria.SSimpleShapeshapes.Behaviors;

public interface IShapeExtentType
{
    bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode);
    IEnumerable<ShapeExtentTypeCode> GetShapeExtentTypeCodes();
    bool IsValidShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode);

    //void ValidateShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode, string paramName);
}
