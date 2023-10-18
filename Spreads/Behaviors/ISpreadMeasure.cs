namespace CsabaDu.FooVaria.Spreads.Behaviors
{
    public interface ISpreadMeasure<in T, out U> : IQuantifiable<double>, ISpreadMeasure where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
    {
        U GetMeasureUnit();

        void ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure);
    }

    public interface IShapeExtents
    {
        void ValidateShapeExtents(params IExtent[] shapeExtents);
    }
}

