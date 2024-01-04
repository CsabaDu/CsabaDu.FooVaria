using CsabaDu.FooVaria.RateComponents.Factories;

namespace CsabaDu.FooVaria.Masses.Factories.Implementations
{
    public abstract class MassFactory : IMassFactory
    {
        private protected MassFactory(IProportionFactory proportionFactory, IBodyFactory bodyFactory)
        {
            ProportionFactory = NullChecked(proportionFactory, nameof(proportionFactory));
            BodyFactory = NullChecked(bodyFactory, nameof(bodyFactory));
        }

        public IProportionFactory ProportionFactory { get; init; }
        public IBodyFactory BodyFactory { get; init; }

        public abstract IMass Create(IWeight weight, IBody body);
        public IProportion<WeightUnit, VolumeUnit> CreateDensity(IWeight weight, IBody body)
        {
            IVolume? volume = (IVolume?)body?.GetSpreadMeasure();

            return (IProportion<WeightUnit, VolumeUnit>)ProportionFactory.Create(weight, volume);
        }
        public IMeasureFactory GetMeasureFactory()
        {
            return ProportionFactory.MeasureFactory;
        }

        public virtual IBodyFactory GetBodyFactory()
        {
            return BodyFactory;
        }
    }
}
