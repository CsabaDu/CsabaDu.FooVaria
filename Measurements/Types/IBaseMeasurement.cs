﻿namespace CsabaDu.FooVaria.Measurements.Types;

public interface IBaseMeasurement : IBaseMeasurable, IMeasureUnitCollection, IExchangeRateCollection, ICustomNameCollection/*, IRateComponent*/, IMeasureUnit<Enum>, IExchangeRate
{
    string GetName();
}
