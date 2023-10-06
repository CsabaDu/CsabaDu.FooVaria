using CsabaDu.FooVaria.Spreads.Behaviors;

namespace CsabaDu.FooVaria.Spreads.Types
{
    public interface ISpread : IBaseMeasurable, ISpreadMeasures
    {
        ISpread GetSpread(ISpreadMeasure spreadMeasure);
        ISpread GetSpread(ISpread other);
    }

    public interface ISpread<T, U> : ISpread, ISpreadMeasures<T, U>, IFit<T>, IProportional<T, Enum> where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
    {
        T SpreadMeasure { get; init; }

        ISpread<T, U> GetSpread(T spreadMeasure);
        ISpread<T, U> GetSpread(U measureUnit);
        ISpread<T, U> GetSpread(params IExtent[] shapeExtents);
    }

    public interface ISurface : ISpread<IArea, AreaUnit>
    {
    }

    public interface IBody : ISpread<IVolume, VolumeUnit>
    {
    }

    public interface IBulkSurface : ISurface
    {
    }

    public interface IBulkBody : IBody
    {
    }
}
