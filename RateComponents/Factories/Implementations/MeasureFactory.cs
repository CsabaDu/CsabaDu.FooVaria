﻿using CsabaDu.FooVaria.RateComponents.Types.Implementations;
using CsabaDu.FooVaria.RateComponents.Types.Implementations.MeasureTypes;
using CsabaDu.FooVaria.Measurements.Factories;

namespace CsabaDu.FooVaria.RateComponents.Factories.Implementations;

public sealed class MeasureFactory : RateComponentFactory, IMeasureFactory
{
    #region Constructors
    public MeasureFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Numerator;
    #endregion

    #region Public methods
    public override IMeasure CreateDefault(MeasureUnitTypeCode measureUnitTypeCode)
    {
        Enum measureUnit = measureUnitTypeCode.GetDefaultMeasureUnit();

        return Create((ValueType)DefaultRateComponentQuantity, measureUnit);
    }

    public IMeasure Create(ValueType quantity, Enum measureUnit)
    {
        return NullChecked(measureUnit, nameof(measureUnit)) switch
        {
            AreaUnit areaUnit => new Area(this, quantity, areaUnit),
            Currency currency => new Cash(this, quantity, currency),
            DistanceUnit distanceUnit => new Distance(this, quantity, distanceUnit),
            ExtentUnit extentUnit => new Extent(this, quantity, extentUnit),
            Pieces pieces => new PieceCount(this, quantity, pieces),
            TimePeriodUnit timePeriodUnit => new TimePeriod(this, quantity, timePeriodUnit),
            VolumeUnit volumeUnit => new Volume(this, quantity, volumeUnit),
            WeightUnit weightUnit => new Weight(this, quantity, weightUnit),

            _ => throw InvalidMeasureUnitEnumArgumentException(measureUnit),
        };
    }

    public IMeasure Create(ValueType quantity, string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return Create(quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return Create(quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return Create(quantity, measurement);
    }

    public IMeasure Create(ValueType quantity, IMeasurement measurement)
    {
        Enum measureUnit = NullChecked(measurement, nameof(measurement)).GetMeasureUnit();

        return Create(quantity, measureUnit);
    }

    public override IMeasure Create(IRateComponent baseMeasure)
    {
        var (quantity, measurement) = GetBaseMeasureParams(baseMeasure);
        Enum measureUnit = measurement.GetMeasureUnit();

        return Create(quantity, measureUnit);
    }

    //public override IMeasure Create(IDefaultMeasurable other)
    //{
    //    return NullChecked(other, nameof(other)) switch
    //    {
    //        Measurement measurement => Create(measurement),
    //        BaseMeasure baseMeasure => Create(baseMeasure),
    //        Rate rate => Create(rate.Numerator),

    //        _ => throw new InvalidOperationException(null),
    //    };
    //}
    #endregion
}

