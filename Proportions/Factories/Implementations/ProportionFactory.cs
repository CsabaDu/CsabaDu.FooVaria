namespace CsabaDu.FooVaria.Proportions.Factories.Implementations
{
    public abstract class ProportionFactory : IProportionFactory
    {
        #region Public methods
        public abstract IProportion Create(IBaseRate baseRate);
        public abstract IBaseRate Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        public abstract IProportion Create(IRateComponent numerator, IRateComponent denominator);
        public abstract IBaseRate Create(IBaseMeasure numerator, IMeasurable denominator);
        public abstract IBaseRate Create(IBaseMeasure numerator, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        public abstract IBaseRate Create(IBaseMeasure numerator, Enum denominatorMeasureUnit);

        IProportion IProportionFactory.Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

    public abstract class ProportionFactory<T, U> : ProportionFactory, IProportionFactory<T, U>
        where T : class, IProportion<T, U>, IMeasureProportion
        where U : struct, Enum
    {
        public abstract T Create(IRateComponent numerator, U denominatorMeasureUnit);
    }

    public abstract class ProportionFactory<T, W, U> : ProportionFactory, IProportionFactory<T, W, U>
        where T : class, IProportion<T, W, U>, IMeasureProportion
        where U : struct, Enum
        where W : struct, Enum
    {
        public abstract T Create(IRateComponent numerator, U denominatorMeasureUnit);
        //public abstract TNum Create(IRateComponent numerator, TEnum denominatorMeasureUnit);
        public abstract T Create(T other);
        public abstract T Create(W numeratorMeasureUnit, decimal quantity, U denominatorMeasureUnit);
    }

    //public sealed class DensityFacrory : ProportionFactory<IDensity, WeightUnit, VolumeUnit>, IDensityFactory
    //{

    //}
}

