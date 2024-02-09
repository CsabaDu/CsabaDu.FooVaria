namespace CsabaDu.FooVaria.SimpleShapes.Types
{
    public interface ISimpleShape : IShape, IShapeExtents, IDimensions, IDiagonal,IShapeComponent, IShapeComponents<IExtent>
    {
        ISimpleShape GetSimpleShape(ExtentUnit measureUnit);
        ISimpleShape GetSimpleShape(params IExtent[] shapeExtents);

        ISpreadFactory GetSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();
    }

    public interface ISimpleShape<out TTangent> : ISimpleShape
        where TTangent : class, ISimpleShape, ITangentShape
    {
        TTangent GetOuterTangentShape();
        TTangent GetInnerTangentShape();
    }

}
