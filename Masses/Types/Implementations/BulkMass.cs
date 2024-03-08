
namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal sealed class BulkMass : Mass, IBulkMass
{
    #region Constructors
    public BulkMass(IBulkMass other) : base(other)
    {
        BulkBody = other.BulkBody;
        Factory = other.Factory;
    }

    public BulkMass(IBulkMassFactory factory, IWeight weight, IBody body) : base(factory, weight)
    {
        BulkBody = GetBodyFactory().Create(NullChecked(body, nameof(body)))!;
        Factory = factory;
    }
    #endregion

    #region Properties
    public IBulkBody BulkBody { get; init; }
    public IBulkMassFactory Factory { get; init; }
    #endregion

    #region Public methods
    public override IBulkBody GetBody()
    {
        return BulkBody;
    }

    public IBulkMass GetBulkMass(IWeight weight, IVolume volume)
    {
        return Factory.Create(weight, volume);
    }

    public IBulkMass GetBulkMass(IWeight weight, IBody body)
    {
        return (IBulkMass)Factory.Create(weight, body);
    }

    public IBulkMass GetNew(IBulkMass other)
    {
        return Factory.CreateNew(other);
    }

    #region Override methods
    public override IBulkBodyFactory GetBodyFactory()
    {
        return (IBulkBodyFactory)Factory.GetBodyFactory();
    }

    public override IBulkMassFactory GetFactory()
    {
        return Factory;
    }
    #endregion
    #endregion
}
