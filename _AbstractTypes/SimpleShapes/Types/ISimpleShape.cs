namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Types
{
    public interface ISimpleShape : IShape, IShapeExtents, IDimensions, IDiagonal, IShapeComponent, IShapeComponents<IExtent>
    {
        ISimpleShape GetSimpleShape(ExtentUnit measureUnit);
        ISimpleShape GetSimpleShape(params IExtent[] shapeExtents);

        IBulkSpreadFactory GetBulkSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();
    }

    public interface ISimpleShape<out TTangent> : ISimpleShape
        where TTangent : class, ISimpleShape, ITangentShape
    {
        TTangent GetOuterTangentShape();
        TTangent GetInnerTangentShape();
    }
}
