namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface ISimpleShapeExtent
{
    IExtent GetSimpleShapeExtent(SimpleShapeExtentCode simpleShapeExtentCode);

    void ValidateSimpleShapeExtent(IExtent? simpleShapeExtent, string paramName);
}
