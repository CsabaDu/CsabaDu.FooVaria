namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IShapeExtents : IShapeExtentType, IShapeExtent
{
    IExtent? this[ShapeExtentCode shapeExtentCode] { get; }

    IEnumerable<IExtent> GetShapeExtents();

    void ValidateShapeExtentCount(int count, string name);
    void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string name);
}
