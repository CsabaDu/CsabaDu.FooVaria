using CsabaDu.FooVaria.Measurables.Factories;
using CsabaDu.FooVaria.Shapes.Types;
using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface IShapeFactory : IBaseShapeFactory
    {
        ISpreadFactory SpreadFactory { get; init; }
        ITangentShapeFactory TangentShapeFactory { get; init; }

        IMeasureFactory GetMeasureFactory();
        ISpreadFactory GetSpreadFactory();
        ITangentShapeFactory GetTangentShapeFactory();

        IExtent CreateShapeExtent(ExtentUnit extentUnit, ValueType quantity);
    }

    public interface ITangentShapeFactory : IBaseShapeFactory
    {
    }

    public interface ITangentShapeFactory<out T, U> : ITangentShapeFactory where T : class, IShape, ITangentShape where U : IShape, ITangentShape
    {
        T CreateTangentShape(U shape, SideCode sideCode);
        T CreateOuterTangentShape(U shape);
        T CreateInnerTangentShape(U shape);

        U Create(U other);
    }

    public interface IRectangularShapeFactory : ITangentShapeFactory
    {

    }

    public interface IRectangularShapeFactory<out T, U> : ITangentShapeFactory<T, U>, IRectangularShapeFactory where T : class, IShape, ICircularShape where U : IShape, IRectangularShape
    {
        T CreateInnerTangentShape(U rectangularShape, ComparisonCode comparisonCode);
    }

    public interface IPlaneShapeFactory : IShapeFactory, IFactory<IPlaneShape>
    {
        IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular);
    }

    public interface IRectangleFactory : IPlaneShapeFactory, IRectangularShapeFactory<ICircle, IRectangle>
    {
        IRectangle Create(IExtent length, IExtent width);
    }

    public interface ICircularShapeFactory : ITangentShapeFactory
    {

    }

    public interface ICircularShapeFactory<out T, U> : ITangentShapeFactory<T, U>, ICircularShapeFactory where T : class, IShape, IRectangularShape where U : IShape, ICircularShape
    {
        T CreateInnerTangentShape(U circularShape, IExtent tangentRectangleSide);
    }

    public interface ICircleFactory : IPlaneShapeFactory, ICircularShapeFactory<IRectangle, ICircle>
    {
        ICircle Create(IExtent radius);
    }

    public interface IDryBodyFactory : IShapeFactory, IFactory<IDryBody>
    {
        IPlaneShapeFactory BaseFaceFactory { get; init; }
        
        IDryBody Create(IPlaneShape baseFace, IExtent height);
        IPlaneShapeFactory GetBaseFaceFactory();
    }

    public interface IDryBodyFactory<out T, U> : IDryBodyFactory where T : class, IDryBody, ITangentShape where U : IPlaneShape, ITangentShape
    {
        T Create(U baseFace, IExtent height);
        //U CreateBaseFace(params IExtent[] shapeExtent);
    }

    public interface ICuboidFactory : IDryBodyFactory<ICuboid, IRectangle>, IRectangularShapeFactory<ICylinder, ICuboid>
    {
        //IRectangleFactory RectangleFactory { get; init; }

        ICuboid Create(IExtent length, IExtent width, IExtent height);
        IRectangle CreateBaseFace(IExtent length, IExtent width);
        IRectangle CreateProjection(ICuboid cuboid, ShapeExtentTypeCode perpendicular);
        IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode);
    }

    public interface ICylinderFactory : IDryBodyFactory<ICylinder, ICircle>, ICircularShapeFactory<ICuboid, ICylinder>
    {
        //ICircleFactory CircleFactory { get; init; }

        ICylinder Create(IExtent radius, IExtent height);
        ICircle CreateBaseFace(IExtent radius);
        IRectangle CreateVerticalProjection(ICylinder cylinder);
    }
}
