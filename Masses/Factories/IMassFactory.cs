﻿namespace CsabaDu.FooVaria.Masses.Factories;

public interface IMassFactory : IBaseQuantifiableFactory
{
    IBodyFactory BodyFactory { get; init; }
    IProportionFactory ProportionFactory { get; init; }

    IMass Create(IWeight weight, IBody body);
    IProportion CreateDensity(IMass mass);
    IMeasureFactory GetMeasureFactory();
    IBodyFactory GetBodyFactory();
}
