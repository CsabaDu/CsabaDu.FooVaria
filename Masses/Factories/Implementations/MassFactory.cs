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

        public IProportion<WeightUnit, VolumeUnit> CreateDensity(IMass mass)
        {
            IWeight weight = NullChecked(mass, nameof(mass)).Weight;
            IVolume volume = mass.GetVolume();

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

        public IBaseSpread CreateBaseSpread(ISpreadMeasure spreadMeasure)
        {
            return BodyFactory.CreateBaseSpread(spreadMeasure);
        }
    }

    public sealed class BulkMassFactory : MassFactory, IBulkMassFactory
    {
        public BulkMassFactory(IProportionFactory proportionFactory, IBulkBodyFactory bodyFactory) : base(proportionFactory, bodyFactory)
        {
        }

        public IBulkMass Create(IWeight weight, IVolume volume)
        {
            throw new NotImplementedException();
        }

        public IBulkMass Create(IWeight weight, IBody body)
        {
            throw new NotImplementedException();
        }

        public IBulkMass CreateNew(IBulkMass other)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class DryMassFactory : MassFactory, IDryMassFactory
    {
        public DryMassFactory(IProportionFactory proportionFactory, IDryBodyFactory bodyFactory) : base(proportionFactory, bodyFactory)
        {
        }

        public IDryMass Create(IWeight weight, IDryBody dryBody)
        {
            throw new NotImplementedException();
        }

        public IDryMass Create(IWeight weight, IPlaneShape baseFace, IExtent height)
        {
            throw new NotImplementedException();
        }

        public IDryMass Create(IWeight weight, params IExtent[] shapeExtents)
        {
            throw new NotImplementedException();
        }

        public IDryMass CreateNew(IDryMass other)
        {
            throw new NotImplementedException();
        }
    }
}
