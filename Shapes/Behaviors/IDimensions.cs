namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    //public interface IShape<TNum> : IShape where TNum : class, IShape, IShape
    //{
    //    TNum GetOuterTangentShape();
    //}

    //public interface IRectangularShape<TNum> : IRectangularShape where TNum : class, IShape, ICircularShape
    //{
    //    TNum GetInnerTangentShape(ComparisonCode comparisonCode);
    //}

    //public interface ICircularShape<TNum> : IShape, ICircularShape where TNum : class, IShape, IRectangularShape
    //{
    //    TNum GetInnerTangentShape(IExtent innerTangentRectangleSide);
    //}

    public interface IDimensions
    {
        IEnumerable<IExtent> GetDimensions();
        IEnumerable<IExtent> GetSortedDimensions();
    }
}
