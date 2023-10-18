namespace CsabaDu.FooVaria.Spreads.Behaviors
{
    public interface ISpreadMeasure<out T, in U> : IQuantifiable<double>, ISpreadMeasure where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
    {
        void ValidateSpreadMeasure(IMeasure measure);
    }

    public interface IShapeExtents
    {
        void ValidateShapeExtents(params IExtent[] shapeExtents);
    }
}

