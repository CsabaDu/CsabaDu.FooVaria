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

    public IProportion CreateDensity(IMass mass)
    {
        IWeight weight = NullChecked(mass, nameof(mass)).Weight;
        IVolume volume = mass.GetVolume();

        return (IProportion)ProportionFactory.CreateBaseRate(weight, volume);
    }

    public virtual IBodyFactory GetBodyFactory()
    {
        return BodyFactory;
    }

    public abstract IMass Create(IWeight weight, IBody body);
    public abstract IMeasureFactory GetMeasureFactory();
}
