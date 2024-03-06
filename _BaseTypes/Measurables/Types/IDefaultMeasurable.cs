namespace CsabaDu.FooVaria.BaseTypes.Measurables.Types
{
    public interface IDefaultMeasurable : IMeasurable
    {
        IMeasurable? GetDefault(MeasureUnitCode measureUnitCode);
    }

    public interface IDefaultMeasurable<out TSelf> : IDefaultMeasurable
        where TSelf : class, IMeasurable
    {
        TSelf GetDefault();
    }
}
