namespace CsabaDu.FooVaria.Proportions.Factories
{
    public interface IProportionFactory : IBaseRateFactory
    {
        IProportion Create(IBaseRate baseRate);
        IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
        IProportion Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
        IProportion Create(Enum numeratorMeasureUnit, decimal quantity, Enum denominatorMeasureUnit);

        //IProportion Create(IMeasure numerator, Enum denominatorMeasureUnit);
    }

    //public interface IProportionFactory<T, U, W> : IProportionFactory, IFactory<T> where T : class, IProportion where U : struct, Enum where W : struct, Enum
    //{
    //    T Create(U numeratorMeasureUnit, decimal quantity, W denominatorMeasureUnit);
    //    T Create(IMeasure numerator, W denominatorMeasureUnit);
    //}
}
