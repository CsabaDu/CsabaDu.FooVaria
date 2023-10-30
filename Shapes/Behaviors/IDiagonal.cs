using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IDiagonal
    {
        IExtent GetDiagonal(ExtentUnit extentUnit = default);
    }

    public interface IShapeExtent
    {
        IExtent GetShapeExtent(ShapeExtentTypeCode shapeExtentTypeCode);
    }

    public interface IShapeExtentType
    {
        bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode);
        IEnumerable<ShapeExtentTypeCode> GetShapeExtentTypeCodes();

        void ValidateShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode);
    }

    public interface IShapeExtents : IShapeExtentType, IShapeExtent
    {
        IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] { get; }

        IEnumerable<IExtent> GetShapeExtents();

        void ValidateShapeExtentCount(int count, string name);
        void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string name);
    }
}
