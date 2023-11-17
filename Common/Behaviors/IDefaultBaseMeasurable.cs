namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IDefaultBaseMeasurable<out T> where T : class, ICommonBase
    {
        T GetDefault(MeasureUnitTypeCode measureUnitTypeCode);
    }
}
