namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal sealed class BulkMass : Mass, IBulkMass
{
    #region Constructors
    public BulkMass(IBulkMass other) : base(other)
    {
        BulkBody = other.BulkBody;
    }

    public BulkMass(IBulkMassFactory factory, IWeight weight, IBody body) : base(factory, weight, body)
    {
        BulkBody = GetBodyFactory().Create(NullChecked(body, nameof(body)))!;
    }
    #endregion

    #region Properties
    public IBulkBody BulkBody { get; init; }
    #endregion

    #region Public methods
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
    public override IBulkBody GetBody()
    {
        return BulkBody;
    }

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
