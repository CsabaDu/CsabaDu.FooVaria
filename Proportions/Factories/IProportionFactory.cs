using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types;
using CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

namespace CsabaDu.FooVaria.Proportions.Factories;

public interface IProportionFactory : ISimpleRateFactory
{
    IProportion Create(IQuantifiable numerator, IQuantifiable denominator);
    IProportion Create(IQuantifiable numerator, IBaseMeasurement denominator);
    IProportion Create(IQuantifiable numerator, Enum denominatorContext);
    IProportion Create(Enum numeratorContext, decimal quantity, Enum denominatorContext);
    IProportion Create(IBaseRate baseRate);
}

    //IProportion<TDEnum> Create<TDEnum>(MeasureUnitCode numeratorCode, decimal numeratorDefaultQuantity, TDEnum denominatorMeasureUnit)
    //    where TDEnum : struct, Enum;
    //IProportion<TDEnum> Create<TDEnum>(Enum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
    //    where TDEnum : struct, Enum;
    //IProportion<TDEnum> Create<TDEnum>(IBaseMeasure numerator, TDEnum denominatorMeasureUnit)
    //    where TDEnum : struct, Enum;

    //IProportion<TNEnum, TDEnum> Create<TNEnum, TDEnum>(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit)
    //    where TNEnum : struct, Enum
    //    where TDEnum : struct, Enum;