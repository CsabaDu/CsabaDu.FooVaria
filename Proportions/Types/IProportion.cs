using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types;
using CsabaDu.FooVaria.BaseTypes.Quantifiables.Types;

namespace CsabaDu.FooVaria.Proportions.Types;

public interface IProportion : ISimpleRate
{
    IProportion GetProportion(IQuantifiable numerator, IQuantifiable denominator);
    IProportion GetProportion(IQuantifiable numerator, IBaseMeasurement denominator);
    IProportion GetProportion(IQuantifiable numerator, Enum denominatorContext);
    IProportion GetProportion(Enum numeratorContext, decimal quantity, Enum denominatorContext);
    IProportion GetProportion(IBaseRate baseRate);
}


//IProportion GetProportion(IBaseRate baseRate);
//decimal GetQuantity(Enum context);
//decimal GetQuantity(Enum numeratorContext, Enum denominatorContext);


//MeasureUnitCode NumeratorCode { get; init; }
//decimal DefaultQuantity { get; init; }

//public interface IProportion<TDEnum> : IProportion, IDenominate<IMeasure, TDEnum>
//    where TDEnum : struct, Enum
//{
//    IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator);
//    IProportion<TDEnum> GetProportion(IBaseMeasure numerator, TDEnum denominatorMeasureUnit);
//    decimal GetQuantity(TDEnum denominatorMeasureUnit);
//}

//public interface IProportion<TNEnum, TDEnum> : IProportion<TDEnum>, IMeasureUnit<TNEnum>
//    where TNEnum : struct, Enum
//    where TDEnum : struct, Enum
//{
//    IProportion<TNEnum, TDEnum> GetProportion(TNEnum numeratorMeasureUnit, ValueType quantity, TDEnum denominatorMeasureUnit);
//    decimal GetQuantity(TNEnum numeratorMeasureUnit, TDEnum denominatorMeasureUnit);
//    decimal GetQuantity(TNEnum numeratorMeasureUnit);
//}