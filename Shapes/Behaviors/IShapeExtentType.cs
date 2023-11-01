using System.Diagnostics.CodeAnalysis;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IShapeExtentType
    {
        bool TryGetShapeExtentTypeCode(IExtent shapeExtent, [NotNullWhen(true)] out ShapeExtentTypeCode? shapeExtentTypeCode);
        IEnumerable<ShapeExtentTypeCode> GetShapeExtentTypeCodes();

        void ValidateShapeExtentTypeCode(ShapeExtentTypeCode shapeExtentTypeCode);
    }
}
