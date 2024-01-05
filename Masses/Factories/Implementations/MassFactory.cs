namespace CsabaDu.FooVaria.Masses.Factories.Implementations;

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

    public abstract IMass Create(IWeight weight, IBody body);
}
