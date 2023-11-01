namespace CsabaDu.FooVaria.Common.Factories;

public interface IBaseRateFactory : IBaseMeasurableFactory
{
    IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IBaseRate Create(IQuantifiable numerator, Enum denominatorMeasureUnit);
}