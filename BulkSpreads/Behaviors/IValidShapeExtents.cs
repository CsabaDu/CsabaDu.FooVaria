namespace CsabaDu.FooVaria.BulkSpreads.Behaviors;

public interface IValidShapeExtents
{
    bool AreValidShapeExtents(params IExtent[] shapeExtents);
}
