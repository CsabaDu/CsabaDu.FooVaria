﻿namespace CsabaDu.FooVaria.Shapes.Behaviors;

public interface IShapeExtents : IShapeExtentType, IShapeExtent
{
    IExtent? this[ShapeExtentCode shapeExtentCode] { get; }

    IEnumerable<IExtent> GetShapeExtents();

    void ValidateShapeExtentCount(int count, string paramName);
    void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string paramName);
}
