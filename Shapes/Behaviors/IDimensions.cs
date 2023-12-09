namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    //public interface ITangentShape<TNum> : IShape where TNum : class, IShape, ITangentShape
    //{
    //    TNum GetOuterTangentShape();
    //}

    //public interface IRectangularShape<TNum> : IRectangularShape where TNum : class, IShape, ICircularShape
    //{
    //    TNum GetInnerTangentShape(ComparisonCode comparisonCode);
    //}

    //public interface ICircularShape<TNum> : ITangentShape, ICircularShape where TNum : class, IShape, IRectangularShape
    //{
    //    TNum GetInnerTangentShape(IExtent innerTangentRectangleSide);
    //}

    public interface IDimensions
    {
        IEnumerable<IExtent> GetDimensions();
        IEnumerable<IExtent> GetSortedDimensions();
    }
}
