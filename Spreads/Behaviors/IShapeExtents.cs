namespace CsabaDu.FooVaria.Spreads.Behaviors;

public interface IShapeExtents
{
    void ValidateShapeExtents(string paramName, params IExtent[] shapeExtents);
}

