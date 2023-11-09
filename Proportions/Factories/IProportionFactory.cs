namespace CsabaDu.FooVaria.Proportions.Factories
{
    public interface IProportionFactory : IBaseRateFactory
    {
        IProportion Create(IBaseRate baseRate);
        IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        IProportion Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement);

        //IProportion Create(Enum numeratorMeasureUnit, decimal quantity, Enum denominatorMeasureUnit);
        //IProportion Create(IMeasure numerator, Enum denominatorMeasureUnit);
    }

    public interface IProportionFactory<T, U> : IProportionFactory where T : class, IProportion where U : struct, Enum
    {
        T Create(IMeasure numerator, U denominatorMeasureUnit);
    }

    public interface IProportionFactory<T, W, U> : IProportionFactory<T, U>, IFactory<T> where T : class, IProportion<T, U> where U : struct, Enum where W : struct, Enum
    {
        T Create(W numeratorMeasureUnit, decimal quantity, U denominatorMeasureUnit);
    }
}
