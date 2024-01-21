﻿namespace CsabaDu.FooVaria.Measurements.Factories;

public interface IMeasurementFactory : IBaseMeasurementFactory, IFactory<IMeasurement>, IDefaultMeasurableFactory
{
    IMeasurement Create(IBaseMeasurement baseMeasurement);
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(string name);
    IMeasurement? Create(string customName, MeasureUnitCode measureUnitCode, decimal exchangeRate);
    IMeasurement? Create(Enum measureUnit, decimal exchangeRate, string customName);
}
