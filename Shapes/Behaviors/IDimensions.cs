namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    //public interface ITangentShape<T> : IShape where T : class, IShape, ITangentShape
    //{
    //    T GetOuterTangentShape();
    //}

    //public interface IRectangularShape<T> : IRectangularShape where T : class, IShape, ICircularShape
    //{
    //    T GetInnerTangentShape(ComparisonCode comparisonCode);
    //}

    //public interface ICircularShape<T> : ITangentShape, ICircularShape where T : class, IShape, IRectangularShape
    //{
    //    T GetInnerTangentShape(IExtent innerTangentRectangleSide);
    //}

    public interface IDimensions
    {
        IEnumerable<IExtent> GetDimensions();
        IEnumerable<IExtent> GetSortedDimensions();
    }
}
