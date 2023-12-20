namespace CsabaDu.FooVaria.Proportions.Types
{
    public interface IProportion : IBaseRate
    {
        MeasureUnitTypeCode NumeratorMeasureUnitTypeCode { get; init; }

        IProportion GetProportion(IBaseRate baseRate);
        IProportion GetProportion(IRateComponent numerator, IRateComponent denominator);
    }

    public interface IProportion<out T, in U> : IProportion, IDenominate<U, IMeasure>
        where T : class, IProportion, IMeasureProportion
        where U : struct, Enum
    {
        T GetProportion(IRateComponent numerator, U denominatorMeasureUnit);
        decimal GetQuantity(U denominatorMeasureUnit);
    }

    public interface IProportion<out T, in W, in U> : IProportion<T, U>
        where T : class, IProportion<T, W, U>, IMeasureProportion
        where U : struct, Enum
        where W : struct, Enum
    {
        T GetProportion(W numeratorMeasureUnit, ValueType quantity, U denominatorMeasureUnit);
        decimal GetQuantity(W numeratorMeasureUnit, U denominatorMeasureUnit);
    }
}
