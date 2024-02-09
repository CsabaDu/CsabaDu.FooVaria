namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface ISimpleShapeExtentType
{
    bool TryGetSimpleShapeExtentCode(IExtent simpleShapeExtent, [NotNullWhen(true)] out SimpleShapeExtentCode? simpleShapeExtentCode);
    IEnumerable<SimpleShapeExtentCode> GetSimpleShapeExtentCodes();
    bool IsValidSimpleShapeExtentCode(SimpleShapeExtentCode simpleShapeExtentCode);

    //void ValidateSimpleShapeExtentCode(SimpleShapeExtentCode simpleShapeExtentCode, string paramName);
}
