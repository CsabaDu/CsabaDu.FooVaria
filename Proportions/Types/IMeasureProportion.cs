using CsabaDu.FooVaria.Measurables.Behaviors;

namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IMeasureProportion : IBaseRate
    {
    }

    public interface IMeasureProportion<out T, in U> : IMeasureProportion where T : class, IProportion, IMeasureProportion where U : class, IMeasure, IDefaultBaseMeasure
    {
        //T GetProportion(U numerator, IMeasurement denominatorMeasurement);
        T GetProportion(U numerator, IDenominator denominator);
    }


    public interface IMeasureProportion<out T, in U, in W> : IMeasureProportion<T, U> where T : class, IProportion, IMeasureProportion<T, U> where U : class, IMeasure, IDefaultBaseMeasure where W : class, IMeasure, IDefaultBaseMeasure
    {
        T GetProportion(U numerator, W denominator);
    }
}
