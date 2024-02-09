namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IShapeExtents : ISimpleShapeExtentType, ISimpleShapeExtent
{
    IExtent? this[SimpleShapeExtentCode simpleShapeExtentCode] { get; }

    IEnumerable<IExtent> GetShapeExtents();

    void ValidateSimpleShapeExtentCount(int count, string paramName);
    void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName);
}
