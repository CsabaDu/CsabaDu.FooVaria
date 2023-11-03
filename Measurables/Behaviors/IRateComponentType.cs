﻿namespace CsabaDu.FooVaria.Measurables.Behaviors
{
    public interface IRateComponentType
    {
    }

    public interface IRateComponentType<out T>  where T : class, IMeasurable
    {
        T? GetRateComponent(IRate rate, RateComponentCode rateComponentCode);
    }

    //public interface IDenominate : IMultiply<IMeasure, IMeasure>
    //{

    //}
}
