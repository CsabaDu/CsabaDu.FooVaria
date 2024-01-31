namespace CsabaDu.FooVaria.Spreads.Types
{
    public interface ISpread : IBaseSpread, IValidShapeExtents
    {
        ISpread GetSpread(ISpreadMeasure spreadMeasure);
        ISpread GetSpread(params IExtent[] shapeExtents);
        ISpread GetSpread(IBaseSpread baseSpread);
    }

    public interface ISpread<TSelf, TSMeasure> : ISpread, ICommonBase<TSelf>
        where TSelf : class, ISpread
        where TSMeasure : class, IMeasure<TSMeasure, double>, ISpreadMeasure
    {
        TSMeasure SpreadMeasure { get; init; }

        TSelf GetSpread(TSMeasure spreadMeasure);
    }

    public interface ISpread<TSelf, TSMeasure, TEnum> : ISpread<TSelf, TSMeasure>, ISpreadMeasure<TSMeasure, TEnum>
        where TSelf : class, ISpread
        where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure
        where TEnum : struct, Enum
    {
        TSelf GetSpread(TEnum measureUnit);
        TSelf GetSpread(TEnum measureUnit, double quantity);
    }
}
