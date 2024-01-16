using CsabaDu.FooVaria.Measurables.Types;
using CsabaDu.FooVaria.Quantifiables.Behaviors;
using CsabaDu.FooVaria.Quantifiables.Enums;
using CsabaDu.FooVaria.Quantifiables.Types;

namespace CsabaDu.FooVaria.Rates.Types
{

    public interface IRate : IBaseRate, IExchange<IRate, IMeasurable>, IDenominate<IMeasure, IBaseMeasure>, IEqualityComparer<IRate>
    {
        IDenominator Denominator { get; init; }
        IMeasure Numerator { get; init; }
        new IRateComponent? this[RateComponentCode rateComponentCode] { get; }

        ILimit? GetLimit();
        IRate GetRate(params IRateComponent[] rateComponents);
        IRate GetRate(IBaseRate baseRate);
        IRateComponent GetRateComponent(RateComponentCode rateComponentCode);
    }
}

    //public interface IRate<T> : IRate, ICommonBase<T>
    //    where T : class, IRate
    //{
    //    T GetRate(IRate other);
    //}