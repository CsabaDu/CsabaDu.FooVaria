namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal sealed class BulkMass : Mass, IBulkMass
{
    #region Constructors
    public BulkMass(IBulkMass other) : base(other)
    {
        Body = other.Body;
    }

    public BulkMass(IBulkMassFactory factory, IWeight weight, IBody body) : base(factory, weight, body)
    {
        Body = GetBodyFactory().Create(NullChecked(body, nameof(body)))!;
    }
    #endregion

    #region Properties
    public override IBody Body { get; init; }
    #endregion

    #region Public methods
    public IBulkBody GetBulkBody()
    {
        return (IBulkBody)Body;
    }

    public IBulkMass GetBulkMass(IWeight weight, IVolume volume)
    {
        return GetFactory().Create(weight, volume);
    }

    public IBulkMass GetBulkMass(IWeight weight, IBody body)
    {
        return (IBulkMass)GetFactory().Create(weight, body);
    }

    public IBulkMass GetNew(IBulkMass other)
    {
        return GetFactory().CreateNew(other);
    }

    #region Override methods
    public override IBulkBodyFactory GetBodyFactory()
    {
        return (IBulkBodyFactory)base.GetBodyFactory();
    }

    public override IBulkMassFactory GetFactory()
    {
        return (IBulkMassFactory)Factory;
    }

    public override IBulkMass GetMass(IWeight weight, IBody body)
    {
        return GetBulkMass(weight, body);
    }
    #endregion
    #endregion
}
