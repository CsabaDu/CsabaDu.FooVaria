namespace CsabaDu.FooVaria.Common.Types
{
    public interface IMeasurable : ICommonBase, IDefaultMeasureUnit, IMeasureUnitType, IExchangeable<MeasureUnitTypeCode>
    {
        MeasureUnitTypeCode MeasureUnitTypeCode { get; init; }
    }

    public interface IMeasurable<out T> : IMeasurable where T : class, IMeasurable
    {
        T GetDefault(MeasureUnitTypeCode measureUnitTypeCode);
    }
}
