namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IShape : IBaseShape, IShapeExtents, IDimensions, IDiagonal,IShapeComponent, IShapeComponents<IExtent>
    {
        IShape GetShape(ExtentUnit measureUnit);
        IShape GetShape(params IExtent[] shapeExtents);

        ISpreadFactory GetSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();
    }

    public interface IShape<out TTangent> : IShape
        where TTangent : class, IShape, ITangentShape
    {
        TTangent GetOuterTangentShape();
        TTangent GetInnerTangentShape();
    }

}
