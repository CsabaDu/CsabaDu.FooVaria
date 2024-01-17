namespace CsabaDu.FooVaria.BaseMeasures.Factories
{
    public interface IDefaultBaseMeasureFactory : IBaseMeasureFactory
    {
    }

    public interface IDefaultBaseMeasureFactory<T> : IDefaultBaseMeasureFactory, IDefaultMeasurableFactory<T>/*, IFactory<T>*/
        where T : class, IBaseMeasure
    {
    }
}

