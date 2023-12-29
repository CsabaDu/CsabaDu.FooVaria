using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Proportions.Factories;

public interface IProportionFactory : IBaseRateFactory
{
    IMeasureFactory MeasureFactory { get; init; }

    IProportion Create(IBaseRate baseRate);
    IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    IProportion Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit);

    IProportion<TDEnum> Create<TDEnum>(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum;
    IProportion<TDEnum> Create<TDEnum>(Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum;
    IProportion<TDEnum> Create<TDEnum>(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum;

    IProportion<TNEnum, TDEnum> Create<TNEnum, TDEnum>(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum;
}
