using CsabaDu.FooVaria.Measurables.Behaviors;

namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate, IDenominate
    {
        MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }

        IProportion GetProportion(IBaseRate baseRate);
        IProportion GetProportion(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
    }

    public interface IProportion<out T, in U, in W> : IProportion where T : class, IProportion where U : struct, Enum where W : struct, Enum
    {
        T GetProportion(U numeratorMeasureUnit, ValueType quantity, W denominatorMeasureUnit);
        T GetProportion(IMeasure numerator, W denominatorMeasureUnit);
        decimal GetQuantity(U numeratorMeasureUnit, W denominatorMeasureUnit);
    }
}
