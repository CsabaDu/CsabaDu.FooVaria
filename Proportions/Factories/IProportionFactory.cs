namespace CsabaDu.FooVaria.Proportions.Factories;

public interface IProportionFactory : ISimpleRateFactory
{
    IMeasureFactory MeasureFactory { get; init; }

    IProportion Create(IBaseRate baseRate);
    IProportion Create(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
    IProportion Create(Enum numeratorMeasureUnit, ValueType quantity, Enum denominatorMeasureUnit);
    IProportion Create(IBaseMeasure numerator, IBaseMeasure denominator);

    IProportion<TDEnum> Create<TDEnum>(MeasureUnitCode numeratorCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum;
    IProportion<TDEnum> Create<TDEnum>(Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum;
    IProportion<TDEnum> Create<TDEnum>(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
        where TDEnum : struct, Enum;

    IProportion<TNEnum, TDEnum> Create<TNEnum, TDEnum>(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
        where TNEnum : struct, Enum
        where TDEnum : struct, Enum;
}
