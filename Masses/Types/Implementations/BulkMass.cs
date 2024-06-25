namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal sealed class BulkMass : Mass, IBulkMass
{
    #region Constructors
    public BulkMass(IBulkMass other) : base(other)
    {
        BulkBody = other.BulkBody;
        //Factory = other.Factory;
    }

    public BulkMass(IBulkMassFactory factory, IWeight weight, IBody body) : base(factory, weight)
    {
        BulkBody = GetBodyFactory().Create(NullChecked(body, nameof(body)))!;
        //Factory = factory;
    }
    #endregion

    #region Properties
    public IBulkBody BulkBody { get; init; }
    //public IBulkMassFactory Factory { get; init; }
    #endregion

    #region Public methods
    #region Override methods
    public override IBulkBody GetBody()
    {
        return BulkBody;
    }

    public override IBulkBodyFactory GetBodyFactory()
    {
        return (IBulkBodyFactory)GetFactory().GetBodyFactory();
    }

    public IBulkMassFactory GetFactory()
    {
        return (IBulkMassFactory)Factory;
    }

    public override IMass GetMass(IBody body, IProportion density)
    {
        return GetMass(this, body, density);
    }

    public override bool TryExchangeTo(Enum? context, [NotNullWhen(true)] out IMass? exchanged)
    {
        if (Weight.IsExchangeableTo(context)) return base.TryExchangeTo(context, out exchanged);

        exchanged = null;

        if (!BulkBody.IsExchangeableTo(context)) return false;

        MeasureUnitElements measureUnitElements = GetMeasureUnitElements(context, nameof(context));

        if (measureUnitElements.MeasureUnit is not VolumeUnit volumeUnit) return false;

        exchanged = ExchangeTo(volumeUnit);

        return exchanged is not null;
    }
    #endregion

    public IBulkMass? ExchangeTo(VolumeUnit volumeUnit)
    {
        IBody? body = BulkBody.ExchangeTo(volumeUnit);

        if (body is null) return null;

        return GetBulkMass(Weight, body);
    }

    public IBulkMass GetBulkMass(IWeight weight, IProportion density)
    {
        decimal defaultQuantity = NullChecked(weight, nameof(weight)).GetDefaultQuantity();

        ValidateDensity(density, nameof(density));

        defaultQuantity *= density.DefaultQuantity;
        IVolume volume = (IVolume)GetVolume().GetBaseMeasure(default(VolumeUnit), defaultQuantity);

        return GetBulkMass(weight, volume);
    }

    public IBulkMass GetBulkMass(IVolume volume, IProportion density)
    {
        decimal defaultQuantity = NullChecked(volume, nameof(volume)).GetDefaultQuantity();

        ValidateDensity(density, nameof(density));

        defaultQuantity *= density.DefaultQuantity;
        IWeight weight = (IWeight)Weight.GetBaseMeasure(default(WeightUnit), defaultQuantity);

        return GetBulkMass(weight, volume);
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
    #endregion
}
