namespace CsabaDu.FooVaria.Masses.Factories.Implementations;

public sealed class BulkMassFactory : MassFactory, IBulkMassFactory
{
    #region Constructors
    public BulkMassFactory(IProportionFactory proportionFactory, IBulkBodyFactory bodyFactory) : base(proportionFactory, bodyFactory)
    {
    }
    #endregion

    #region Public methods
    public IBulkMass Create(IWeight weight, IVolume volume)
    {
        IBody body = GetBodyFactory().Create(volume);

        return Create(weight, body);
    }

    public IBulkMass CreateNew(IBulkMass other)
    {
        return new BulkMass(other);
    }

    #region Override methods
    public override IBulkMass Create(IWeight weight, IBody body)
    {
        return new BulkMass(this, weight, body);
    }

    public override IBulkBodyFactory GetBodyFactory()
    {
        return (IBulkBodyFactory)BodyFactory;
    }
    #endregion
    #endregion
}
