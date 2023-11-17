namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IRateComponent
    {
    }

    public interface IRateComponent<out T>  where T : class, IMeasurable
    {
        T? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
    }

    //public interface IDenominate : IMultiply<IMeasure, IMeasure>
    //{

    //}
}
