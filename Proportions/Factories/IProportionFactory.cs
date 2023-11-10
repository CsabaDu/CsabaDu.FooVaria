using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Factories
{
    public interface IProportionFactory : IBaseRateFactory
    {
        IProportion Create(IBaseRate baseRate);
        IProportion Create(IBaseMeasure numerator, IMeasurement denominatorMeasurement);
        IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    }

    public interface IProportionFactory<T, U> : IProportionFactory where T : class, IProportion where U : struct, Enum
    {
        T Create(IMeasure numerator, U denominatorMeasureUnit);
    }

    public interface IProportionFactory<T, W, U> : IProportionFactory<T, U>, IFactory<T> where T : class, IProportion<T, U> where U : struct, Enum where W : struct, Enum
    {
        T Create(W numeratorMeasureUnit, decimal quantity, U denominatorMeasureUnit);
    }

    public interface IFrequencyFactory : IProportionFactory<IFrequency, Pieces, TimePeriodUnit>
    {
        IFrequency Create(IPieceCount pieceCount, TimePeriodUnit timePeriodUnit);
    }

    public interface IValuabilityFactory : IProportionFactory<IValuability, Currency, WeightUnit>
    {
        IFrequency Create(ICash cash, WeightUnit weightUnit);
    }

    public interface IDensityFactory : IProportionFactory<IDensity, WeightUnit, VolumeUnit>
    {
        IFrequency Create(IWeight weight, VolumeUnit volumeUnit);
    }
}
