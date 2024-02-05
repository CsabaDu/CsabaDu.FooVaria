namespace CsabaDu.FooVaria.Masses.Types.Implementations;

internal sealed class DryMass : Mass, IDryMass
{
    #region Constructors
    internal DryMass(IDryMass other) : base(other)
    {
        DryBody = other.DryBody;
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, IDryBody dryBody) : base(factory, weight, dryBody)
    {
        DryBody = dryBody;
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, params IExtent[] shapeExtents) : base(factory, weight, shapeExtents)
    {
        DryBody = getDryBody();

        #region Local methods
        IDryBody getDryBody()
        {
            IBaseShape? baseShape = GetBodyFactory().CreateBaseShape(shapeExtents);

            if (baseShape is IDryBody dryBody) return dryBody;

            throw CountArgumentOutOfRangeException(shapeExtents.Length, nameof(shapeExtents));
        }
        #endregion
    }

    internal DryMass(IDryMassFactory factory, IWeight weight, IPlaneShape baseFace, IExtent height) : base(factory, weight, baseFace, height)
    {
        DryBody = GetBodyFactory().Create(baseFace, height);
    }
    #endregion

    #region Properties
    public IDryBody DryBody { get; init; }
    #endregion

    #region Public methods
    public IDryMass GetDryMass(IWeight weight, IDryBody dryBody)
    {
        return GetFactory().Create(weight, dryBody);
    }

    public IDryMass GetDryMass(IWeight weight, IPlaneShape baseFace, IExtent height)
    {
        return GetFactory().Create(weight, baseFace, height);
    }

    public IDryMass GetDryMass(IWeight weight, params IExtent[] shapeExtents)
    {
        return GetFactory().Create(weight, shapeExtents);
    }

    public IDryMass GetNew(IDryMass other)
    {
        return GetFactory().CreateNew(other);
    }

    #region Override methods
    public override bool Equals(IBaseSpread? other)
    {
        return base.Equals(other)
            && other is IDryMass dryMass
            && DryBody.Equals(dryMass.DryBody);
    }

    public override IBaseSpread? ExchangeTo(Enum measureUnit)
    {
        if (measureUnit is not ExtentUnit extentUnit) return base.ExchangeTo(measureUnit);

        IBaseSpread? baseSpread = DryBody.ExchangeTo(extentUnit);

        return baseSpread is IDryBody dryBody ?
            GetDryMass(Weight, dryBody)
            : null;
    }

    public override bool? FitsIn(IBaseSpread? baseSpread, LimitMode? limitMode)
    {
        bool? massFitsIn = base.FitsIn(baseSpread, limitMode);

        if (baseSpread is not IDryMass dryMass) return massFitsIn;

        bool? dryBodyFitsIn = GetBody().FitsIn(dryMass.GetBody(), limitMode);

        return BothFitIn(massFitsIn, dryBodyFitsIn);
    }

    public override IDryBody GetBody()
    {
        return DryBody;
    }

    public override IDryBodyFactory GetBodyFactory()
    {
        return (IDryBodyFactory)base.GetBodyFactory();
    }

    public override IDryMassFactory GetFactory()
    {
        return (IDryMassFactory)Factory;
    }

    public override IEnumerable<MeasureUnitCode> GetMeasureUnitCodes()
    {
        return base.GetMeasureUnitCodes().Append(MeasureUnitCode.ExtentUnit);
    }

    public override IDryMass GetMass(IWeight weight, IBody body)
    {
        string paramName = nameof(body);

        if (NullChecked(body, paramName) is IDryBody dryBody) return GetDryMass(weight, dryBody);

        throw ArgumentTypeOutOfRangeException(paramName, body);
    }
    #endregion
    #endregion
}
