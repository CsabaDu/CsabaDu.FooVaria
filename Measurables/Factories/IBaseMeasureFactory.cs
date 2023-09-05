namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IBaseMeasureFactory : IMeasurableFactory, IRateComponentFactory<IBaseMeasure>
{
    IMeasurementFactory MeasurementFactory { get; init; }
    RateComponentCode RateComponentCode { get; }

    IBaseMeasure Create(IBaseMeasureFactory baseMeasureFactory, IBaseMeasure baseMeasure);
}

    //IBaseMeasure CreateDefault(RateComponentCode rateComponentCode, MeasureUnitTypeCode measureUnitTypeCode);