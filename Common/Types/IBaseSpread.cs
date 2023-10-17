namespace CsabaDu.FooVaria.Common.Types
{
    public interface IBaseSpread : IBaseMeasurable, ISpreadMeasure
    {
        //IEnumerable<MeasureUnitTypeCode> SpreadMeasureUnitTypeCodes { get; }
    }

    public interface IBaseSpread<out T> : IBaseSpread where T : class, IBaseSpread
    {
        T GetBaseSpread(IBaseSpread other);
    }
}

