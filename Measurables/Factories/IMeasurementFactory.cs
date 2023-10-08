﻿namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IMeasurementFactory : IBaseMeasurementFactory, IDefaultRateComponentFactory<IMeasurement>
{
    IMeasurement Create(string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate);
    IMeasurement Create(Enum measureUnit, decimal exchangeRate, string customName);
    IMeasurement Create(Enum measureUnit);
    IMeasurement Create(string name);
    IMeasurement Create(IMeasurement measurement);
}
