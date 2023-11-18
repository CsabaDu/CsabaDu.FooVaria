using CsabaDu.FooVaria.RateComponents.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Proportions.Factories
{
    public interface IProportionFactory : IBaseRateFactory
    {
        IProportion Create(IBaseRate baseRate);
        IProportion Create(IRateComponent numerator, IRateComponent denominator);
        IProportion Create(MeasureUnitTypeCode numeratorMeasureUnitTypeCode, decimal defaultQuantity, MeasureUnitTypeCode denominatorMeasureUnitTypeCode);
    }

    public interface IProportionFactory<out T, in U> : IProportionFactory where T : class, IProportion, IMeasureProportion where U : struct, Enum
    {
        T Create(IRateComponent numerator, U denominatorMeasureUnit);
    }

    public interface IProportionFactory<T, in W, in U> : IProportionFactory<T, U>, IFactory<T> where T : class, IProportion<T, U>, IMeasureProportion where U : struct, Enum where W : struct, Enum
    {
        T Create(W numeratorMeasureUnit, decimal quantity, U denominatorMeasureUnit);
    }

    public interface IFrequencyFactory : IProportionFactory<IFrequency, Pieces, TimePeriodUnit>, IMeasureProportionFactory<IFrequency, IPieceCount, ITimePeriod>
    {
    }

    public interface IValuabilityFactory : IProportionFactory<IValuability, Currency, WeightUnit>, IMeasureProportionFactory<IValuability, ICash, IWeight>
    {
    }

    public interface IDensityFactory : IProportionFactory<IDensity, WeightUnit, VolumeUnit>, IMeasureProportionFactory<IDensity, IWeight, IVolume>
    {
    }

    public interface IMeasureProportionFactory : IBaseRateFactory
    {
    }

    public interface IMeasureProportionFactory<out T, in U> : IMeasureProportionFactory where T : class, IProportion, IMeasureProportion<T, U> where U : class, IMeasure, IDefaultRateComponent
    {
        T GetProportion(U numerator, IMeasurement denominatorMeasurement);
        T GetProportion(U numerator, IDenominator denominator);
    }


    public interface IMeasureProportionFactory<out T, in U, in W> : IMeasureProportionFactory<T, U> where T : class, IProportion, IMeasureProportion<T, U, W> where U : class, IMeasure, IDefaultRateComponent where W : class, IMeasure, IDefaultRateComponent
    {
        T GetProportion(U numerator, W denominator);
    }

}
