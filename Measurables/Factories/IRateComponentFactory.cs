namespace CsabaDu.FooVaria.Measurables.Factories;

public interface IRateComponentFactory<out T> where T : class, IMeasurable, IRateComponent
{
    RateComponentCode GetValidRateComponentCode(RateComponentCode rateComponentCode);

    //T CreateDefault(MeasureUnitTypeCode measureUnitTypeCode);
}
