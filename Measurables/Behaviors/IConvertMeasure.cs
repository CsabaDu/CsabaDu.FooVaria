﻿namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IConvertMeasure<T, U> where T : class, IMeasure where U : notnull
{
    T ConvertFrom(U other);
    U ConvertMeasure(T convertibleMeasure);
}
