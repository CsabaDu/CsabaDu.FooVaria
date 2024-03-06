namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Behaviors;

public interface IShapeExtents : IShapeExtentType, IShapeExtent
{
    IExtent? this[ShapeExtentCode shapeExtentCode] { get; }

    IEnumerable<IExtent> GetShapeExtents();

    void ValidateShapeExtentCount(int count, [DisallowNull] string paramName);
    void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, [DisallowNull] string paramName);
}
