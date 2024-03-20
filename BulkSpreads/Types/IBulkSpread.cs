using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

namespace CsabaDu.FooVaria.BulkSpreads.Types
{
    public interface IBulkSpread : ISpread, IValidShapeExtents
    {
        IBulkSpread GetBulkSpread(ISpreadMeasure spreadMeasure);
        IBulkSpread GetBulkSpread(params IExtent[] shapeExtents);
        IBulkSpread GetBulkSpread(ISpread spread);
    }

    public interface IBulkSpread<TSelf, TSMeasure> : IBulkSpread, ICommonBase<TSelf>
        where TSelf : class, IBulkSpread
        where TSMeasure : class, IMeasure<TSMeasure, double>, ISpreadMeasure
    {
        TSMeasure SpreadMeasure { get; init; }

        TSelf GetBulkSpread(TSMeasure spreadMeasure);
    }

    public interface IBulkSpread<TSelf, TSMeasure, TEnum> : IBulkSpread<TSelf, TSMeasure>, ISpreadMeasure<TSMeasure, TEnum>, IExchange<TSelf, TEnum>
        where TSelf : class, IBulkSpread
        where TSMeasure : class, IMeasure<TSMeasure, double, TEnum>, ISpreadMeasure
        where TEnum : struct, Enum
    {
        TSelf GetBulkSpread(TEnum measureUnit);
        TSelf GetBulkSpread(TEnum measureUnit, double quantity);
    }
}
