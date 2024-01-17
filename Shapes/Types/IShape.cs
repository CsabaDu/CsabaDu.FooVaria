namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IShape : IBaseShape, IShapeExtents, IDimensions, IDiagonal,IShapeComponent, IShapeComponents<IExtent>
    {
        IShape GetSimpleShape(ExtentUnit measureUnit);
        IShape GetSimpleShape(params IExtent[] shapeExtents);

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
