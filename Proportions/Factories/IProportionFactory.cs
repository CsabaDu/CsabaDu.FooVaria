using CsabaDu.FooVaria.Proportions.Types;

namespace CsabaDu.FooVaria.Proportions.Factories
{
    public interface IProportionFactory : IBaseRateFactory
    {
        IProportion Create(IBaseRate baseRate);
        IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode measureUnitTypeCode);
        IProportion Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
    }

    public interface IProportionFactory<T, U, W> where T : class, IProportion where U : struct, Enum where W : struct, Enum
    {
        T Create(U numeratorMeasureUnit, decimal quantity, W denominatorMeasureUnit);
        T Create(IMeasure numerator, W denominatorMeasureUnit);
    }

    public interface IDensityFactory : IProportionFactory, IProportionFactory<IDensity, WeightUnit, VolumeUnit>
    {
    }
}
