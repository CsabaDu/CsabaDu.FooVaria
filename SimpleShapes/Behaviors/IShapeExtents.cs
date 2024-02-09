namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IShapeExtents : IShapeExtentType, IShapeExtent
{
    IExtent? this[ShapeExtentCode simpleShapeExtentCode] { get; }

    IEnumerable<IExtent> GetShapeExtents();

    void ValidateShapeExtentCount(int count, string paramName);
    void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName);
}
