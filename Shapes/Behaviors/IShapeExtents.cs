namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IShapeExtents : IShapeExtentType, IShapeExtent
    {
        IExtent? this[ShapeExtentTypeCode shapeExtentTypeCode] { get; }

        IEnumerable<IExtent> GetShapeExtents();

        void ValidateShapeExtentCount(int count, string name);
        void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents, string name);
    }
}
