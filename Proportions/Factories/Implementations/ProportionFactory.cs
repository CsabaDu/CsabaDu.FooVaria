using CsabaDu.FooVaria.Proportions.Types.Implementations;
using CsabaDu.FooVaria.Proportions.Types.Implementations.ProportionTypes;
using static CsabaDu.FooVaria.Common.Statics.MeasureUnitTypes;
using static CsabaDu.FooVaria.Measurements.Statics.MeasureUnits;

namespace CsabaDu.FooVaria.Proportions.Factories.Implementations
{
    public abstract class ProportionFactory : IProportionFactory
    {
        #region Public methods
        public abstract IProportion Create(IBaseRate baseRate);
        public abstract IProportion Create(IBaseMeasure numerator, IBaseMeasure denominator);
        public abstract IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        public abstract IBaseRate Create(IQuantifiable numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        public abstract IBaseRate Create(IQuantifiable numerator, Enum denominatorMeasureUnit);
        #endregion
    }

    public abstract class ProportionFactory<T, U> : ProportionFactory, IProportionFactory<T, U> where T : class, IProportion<T, U>, IMeasureProportion where U : struct, Enum
    {
        public abstract T Create(IMeasure numerator, U denominatorMeasureUnit);
    }

    public abstract class ProportionFactory<T, W, U> : ProportionFactory, IProportionFactory<T, W, U> where T : class, IProportion<T, W, U>, IMeasureProportion where U : struct, Enum where W : struct, Enum
    {
        public abstract T Create(IMeasure numerator, U denominatorMeasureUnit);
        public abstract T Create(U numeratorMeasureUnit, decimal quantity, W denominatorMeasureUnit);
        public abstract T Create(IMeasure numerator, W denominatorMeasureUnit);
        public abstract T Create(T other);
        public abstract T Create(W numeratorMeasureUnit, decimal quantity, U denominatorMeasureUnit);
    }

}

