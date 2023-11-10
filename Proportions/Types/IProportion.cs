using CsabaDu.FooVaria.Measurables.Behaviors;

namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate, IDenominate
    {
        MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }

        IProportion GetProportion(IBaseRate baseRate);
        IProportion GetProportion(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
        IProportion GetProportion(IBaseMeasure numerator, IBaseMeasure denominator);
    }

    public interface IProportion<out T, in U> : IProportion where T : class, IProportion where U : struct, Enum
    {
        T GetProportion(IMeasure numerator, U denominatorMeasureUnit);
        decimal GetQuantity(U denominatorMeasureUnit);
    }

    public interface IProportion<out T, in W, in U> : IProportion<T, U> where T : class, IProportion<T, W, U> where U : struct, Enum where W : struct, Enum
    {
        T GetProportion(W numeratorMeasureUnit, ValueType quantity, U denominatorMeasureUnit);
        decimal GetQuantity(W numeratorMeasureUnit, U denominatorMeasureUnit);
    }

    public interface IMeasuresProportion : IBaseRate
    {
    }

    public interface IMeasuresProportion<T, U, W> : IMeasuresProportion where T : class, IProportion, IMeasuresProportion where U : class, IMeasure, IDefaultRateComponent where W : class, IMeasure, IDefaultRateComponent
    {
        T GetProportion(U numerator, W denominator);
    }
}
