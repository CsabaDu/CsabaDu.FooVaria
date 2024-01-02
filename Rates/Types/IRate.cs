namespace CsabaDu.FooVaria.Rates.Types
{

    public interface IRate : IBaseRate, IExchange<IRate, IMeasurable>, IDenominate<IMeasure, IRateComponent>
    {
        IDenominator Denominator { get; init; }
        IMeasure Numerator { get; init; }
        IRateComponent? this[RateComponentCode rateComponentCode] { get; }

        ILimit? GetLimit();
        IRate GetRate(params IRateComponent[] rateComponents);
        IRateComponent GetRateComponent(RateComponentCode rateComponentCode);
    }
}

    //public interface IRate<T> : IRate, ICommonBase<T>
    //    where T : class, IRate
    //{
    //    T GetRate(IRate other);
    //}